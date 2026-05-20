using AutoMapper;
using ApiJBA.DTOs;
using ApiJBA.Entidades;

namespace ApiJBA.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapeos para la entidad Personal
            CreateMap<CreacionDePersonal_Post_DTO, Personal>();
            CreateMap<Personal, CreacionDePersonal_Get_DTO>();
            CreateMap<CreacionDePersonal_Get_DTO, Personal>();
            
            // Mapeo especial para traer únicamente el ID
            CreateMap<Personal, PersonalIdDto>()
                .ForMember(dest => dest.id_p, opt => opt.MapFrom(src => src.ci_p));
        }
    }
}
