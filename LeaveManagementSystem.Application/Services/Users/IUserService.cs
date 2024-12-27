using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Application.Services.Users
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetEmployees();
        Task<ApplicationUser> GetUserById(string userId);
        Task<ApplicationUser> GetUserLoggedIn();
    }
}