using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.WebSockets;
using System.Runtime.InteropServices;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController(ILeaveTypeService _leaveTypeService, 
        ILeaveRequestsService _leaveRequestsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestsService.GetEmployeeLeaveRequests();
            return View(model);
        }
        
        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var leaveTypes = await _leaveTypeService.GetAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            if (await _leaveRequestsService.RequestDateExceedAllocation(model))
            {
                ModelState.AddModelError(string.Empty, "You have exceed your allocation");
                ModelState.AddModelError(nameof(model.EndDate), "The number of days required is invalid.");
            }

            if (ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Create));
            }

            var leaveTypes = await _leaveTypeService.GetAll();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await _leaveRequestsService.CancelLeaveRequest(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestsService.AdminGetAllLeaveRequest();
            return View(model);
        }

        public async Task<IActionResult> Review(int id)
        {
            var model = await _leaveRequestsService.GetLeaveRequestForReview(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
    }
}
