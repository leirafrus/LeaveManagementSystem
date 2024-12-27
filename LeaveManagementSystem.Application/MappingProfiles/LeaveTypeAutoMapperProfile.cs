using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Application.MappingProfiles
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
