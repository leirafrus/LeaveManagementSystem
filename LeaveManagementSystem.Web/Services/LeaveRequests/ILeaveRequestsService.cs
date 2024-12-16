using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
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