using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class LeaveRequestCreateVM  //: IValidatableObject
    {
        [Required]
        [DisplayName("Start Date")]
        //[DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        //[DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        [Required]
        [DisplayName("Desired Leave Type")]
        public int LeaveTypeId { get; set; }

        [DisplayName("Additional Information")]
        public string? RequestComments { get; set; }

        public SelectList? LeaveTypes { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    yield return new ValidationResult("The Start Date Must Be Before the End Date", new[]
        //    { nameof(StartDate), nameof(EndDate) });
        //}
    }
}