using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using LeaveManagementSystem.Web.Models.Periods;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveTypeAutoMapperProfile : Profile
    {
        public LeaveTypeAutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            // Do this if the properties from LeaveType to IndexVM is not the same
            //.ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.NumberOfDays));
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
        }
    }
}
