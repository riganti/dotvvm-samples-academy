using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.Validation;

namespace DotvvmAcademy.BL.Profiles
{
    public class ValidationErrorDtoProfile : Profile
    {
        public ValidationErrorDtoProfile()
        {
            CreateMap<ValidationError, ValidationErrorDto>();
        }
    }
}