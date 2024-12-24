using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveTypes;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService,
        ILeaveTypeService _leaveTypeService) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employeeVM = await _leaveAllocationService.GetEmployees();
            return View(employeeVM);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id)
        {
            await _leaveAllocationService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _leaveAllocationService.GetEmployeeAllocation(id.Value);
            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
        {
            if (await _leaveTypeService.DaysExceedMaximum(allocation.LeaveType.Id, allocation.Days))
            {
                ModelState.AddModelError("Days", "The allocation exceeds the maximum leave type value");
            }

            if (ModelState.IsValid)
            {
                await _leaveAllocationService.EditAllocation(allocation);
                return RedirectToAction(nameof(Details), new { userId = allocation.Employee?.Id });
            }

            var days = allocation.Days;
            allocation = await _leaveAllocationService.GetEmployeeAllocation(allocation.Id);
            allocation.Days = days;

            return View(allocation);
        }

        public async Task<IActionResult> Details(string? userId)
        {
            var employeeVM = await _leaveAllocationService.GetEmployeeAllocations(userId);
            return View(employeeVM);
        }
    }
}
