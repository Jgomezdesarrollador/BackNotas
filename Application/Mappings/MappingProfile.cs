using Application.DTOs.Estudiante;
using Application.DTOs.Nota;
using Application.DTOs.Profesor;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Estudiante, EstudianteDto>().ReverseMap();
            CreateMap<EstudianteCreateDto, Estudiante>();
            CreateMap<EstudianteUpdateDto, Estudiante>();

            CreateMap<Profesor, ProfesorDto>().ReverseMap();
            CreateMap<ProfesorCreateDto, Profesor>();
            CreateMap<ProfesorUpdateDto, Profesor>();

            CreateMap<Nota, NotaDto>()
                .ForMember(dest => dest.NombreEstudiante, opt => opt.MapFrom(src => src.Estudiante.Nombre))
                .ForMember(dest => dest.NombreProfesor, opt => opt.MapFrom(src => src.Profesor.Nombre));

            CreateMap<NotaCreateDto, Nota>();
            CreateMap<NotaUpdateDto, Nota>();
        }
    }
}
