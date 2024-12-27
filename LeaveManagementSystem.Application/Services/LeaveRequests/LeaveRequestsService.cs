using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using LeaveManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Application.Services.LeaveRequests
{
    public class LeaveRequestsService(ApplicationDbContext _context,
        IUserService _userService,
        IMapper _mapper, IPeriodService _periodService,
        ILeaveAllocationService _leaveAllocationsService) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

            await UpdateAllocationDays(leaveRequest, false);
            await _context.SaveChangesAsync();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            var user = await _userService.GetUserLoggedIn();
            leaveRequest.EmployeeId = user.Id;

            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

            _context.Add(leaveRequest);

            await UpdateAllocationDays(leaveRequest, true);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequest()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();

            var approvedRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)(LeaveRequestStatusEnum.Approved));
            var pendingRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)(LeaveRequestStatusEnum.Pending));
            var declinedRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)(LeaveRequestStatusEnum.Declined));

            var leaveRequestModels = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();

            var model = new EmployeeLeaveRequestListVM
            {
                ApprovedRequests = approvedRequestsCount,
                PendingRequests = pendingRequestsCount,
                DeclinedRequests = declinedRequestsCount,
                TotalRequests = leaveRequests.Count,
                LeaveRequests = leaveRequestModels
            };

            return model;
        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userService.GetUserLoggedIn();
            var leaveRequests = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();

            var model = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();

            return model;
        }

        public async Task<bool> RequestDateExceedAllocation(LeaveRequestCreateVM model)
        {
            var user = await _userService.GetUserLoggedIn();
            var period = await _periodService.GetCurrentPeriod();
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations
                .FirstOrDefaultAsync(q => q.LeaveTypeId == model.LeaveTypeId
                && q.EmployeeId == user.Id
                && q.PeriodId == period.Id
                );

            return allocation.Days < numberOfDays;
        }

        public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
        {
            var user = await _userService.GetUserLoggedIn();
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved
                ? (int)LeaveRequestStatusEnum.Approved
                : (int)LeaveRequestStatusEnum.Declined;

            leaveRequest.ReviewerId = user.Id;

            if (approved)
            {
                await UpdateAllocationDays(leaveRequest, false);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstAsync(q => q.Id == id);

            var user = await _userService.GetUserById(leaveRequest.EmployeeId);

            var model = new ReviewLeaveRequestVM
            {
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
                LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
                Id = leaveRequest.Id,
                LeaveType = leaveRequest.LeaveType.Name,
                RequestComments = leaveRequest.RequestComments,
                Employee = new EmployeeListVM
                {
                    Id = leaveRequest.EmployeeId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };

            return model;
        }

        private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
        {
            var allocation = await _leaveAllocationsService.GetCurrentAllocation
                (leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
            var numberOfDays = CalculateDays(leaveRequest.EndDate, leaveRequest.StartDate);

            if (deductDays)
            {
                allocation.Days -= numberOfDays;
            }
            else
            {
                allocation.Days += numberOfDays;
            }

            _context.Entry(allocation).State = EntityState.Modified;
        }

        private int CalculateDays(DateOnly start, DateOnly end)
        {
            return end.DayNumber - start.DayNumber;
        }
    }
}
