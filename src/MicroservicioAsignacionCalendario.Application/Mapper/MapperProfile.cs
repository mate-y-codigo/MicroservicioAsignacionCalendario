using Application.DTOs.AlumnoPlan;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // AlumnoPlan
            CreateMap<AlumnoPlanRequest, AlumnoPlan>()
                .ForMember(
                    dest => dest.Notas,
                    opt => opt.MapFrom(src => src.Notas != null ? src.Notas.Trim() : "")
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => EstadoAlumnoPlan.Activo)
                )

                .ForMember(
                    dest => dest.IdSesionActual,
                    opt => opt.Ignore()
                );
            CreateMap<AlumnoPlan, AlumnoPlanResponse>();

            // EjercicioRegistro
            CreateMap<EjercicioRegistroRequest, EjercicioRegistro>()
                .ForMember(dest => dest.IdSesionRealizada, opt => opt.Ignore())
                .ForMember(dest => dest.PesoObjetivo, opt => opt.Ignore())
                .ForMember(dest => dest.RepeticionesObjetivo, opt => opt.Ignore())
                .ForMember(dest => dest.SeriesObjetivo, opt => opt.Ignore());
            CreateMap<EjercicioRegistro, EjercicioRegistroResponse>();

            // SesionRealizada
            CreateMap<SesionRealizadaRequest, SesionRealizada>()
                .ForMember(dest => dest.IdPlanAlumno, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore())
                .ForMember(dest => dest.EjerciciosRegistrados, opt => opt.Ignore());
            CreateMap<SesionRealizada, SesionRealizadaResponse>();
        }
    }
}
