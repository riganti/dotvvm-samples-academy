using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.Validation;

namespace DotvvmAcademy.BL.Profiles
{
    public class ValidationErrorDtoProfile : Profile
    {
        public ValidationErrorDtoProfile()
        {
            // CreateMap<ValidationDiagnostic, ValidationErrorDto>()
            //     .ForMember(t => t.StartPosition, ex => ex.MapFrom(s => s.Location.Start))
            //     .ForMember(t => t.EndPosition, ex => ex.MapFrom(s => s.Location.End));
        }
    }
}