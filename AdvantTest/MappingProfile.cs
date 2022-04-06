using AdvantTest.Grpc;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AdvantTest
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdvantTest.Domain.Core.Employee, Employee>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.Surname, opt => opt.MapFrom(s => s.Surname))
            .ForMember(d => d.Patronymic, opt => opt.MapFrom(s => s.Patronymic))
            .ForMember(d => d.IsHaveChild, opt => opt.MapFrom(s => s.HasChildren))
            .ForMember(d => d.Birthdate, opt => opt.MapFrom(s => Timestamp.FromDateTimeOffset(s.BirthDate)));

            CreateMap<EmployeeForCreation, Domain.Core.Employee>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Surname, opt => opt.MapFrom(s => s.Surname))
            .ForMember(d => d.Patronymic, opt => opt.MapFrom(s => s.Patronymic))
            .ForMember(d => d.HasChildren, opt => opt.MapFrom(s => s.IsHaveChild))
            .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.Birthdate.ToDateTime()));
        }
    }
}
