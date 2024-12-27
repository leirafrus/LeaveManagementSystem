using LeaveManagementSystem.Application.Models.LeaveRequests;

namespace LeaveManagementSystem.Application.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);

        Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests();

        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequest();

        Task CancelLeaveRequest(int leaveRequestId);

        Task ReviewLeaveRequest(int leaveRequestId, bool approved);

        Task<bool> RequestDateExceedAllocation(LeaveRequestCreateVM model);

        Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id);
    }
}