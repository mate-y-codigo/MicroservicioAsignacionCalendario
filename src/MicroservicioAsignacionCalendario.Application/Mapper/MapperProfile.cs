using Application.DTOs.AlumnoPlan;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
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
                .ForMember(dest => dest.Notas, opt => opt.MapFrom(src => src.Notas != null ? src.Notas.Trim() : ""))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => EstadoAlumnoPlan.Activo))
                .ForMember(dest => dest.IdSesionARealizar, opt => opt.Ignore())
                .ForMember(dest => dest.NombrePlan, opt => opt.Ignore())
                .ForMember(dest => dest.DescripcionPlan, opt => opt.Ignore())
                .ForMember(dest => dest.TotalSesiones, opt => opt.Ignore())
                .ForMember(dest => dest.NombreAlumno, opt => opt.Ignore());
            CreateMap<AlumnoPlan, AlumnoPlanResponse>();

            // SesionRealizada
            CreateMap<SesionRealizadaRequest, SesionRealizada>()
                .ForMember(dest => dest.EjerciciosRegistrados, opt => opt.MapFrom(src => src.RegistroEjercicios))
                .ForMember(dest => dest.IdAlumnoPlan, opt => opt.Ignore())
                .ForMember(dest => dest.NombreSesion, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenSesion, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => EstadoSesion.Completado));
            CreateMap<SesionRealizada, SesionRealizadaResponse>();
            // TO DO: Agregar include alumno plan en la consulta
            CreateMap<SesionRealizada, SesionRealizadaListResponse>()
                .ForMember(dest => dest.NombrePlan, opt => opt.MapFrom(src => src.AlumnoPlan.NombrePlan))
                .ForMember(dest => dest.NombreSesion, opt => opt.MapFrom(src => src.NombreSesion));

            // EjercicioRegistro
            CreateMap<EjercicioRegistroRequest, EjercicioRegistro>()
                .ForMember(dest => dest.IdEjercicioSesion, opt => opt.Ignore())
                .ForMember(dest => dest.IdSesionRealizada, opt => opt.Ignore())
                .ForMember(dest => dest.PesoObjetivo, opt => opt.Ignore())
                .ForMember(dest => dest.RepeticionesObjetivo, opt => opt.Ignore())
                .ForMember(dest => dest.DescansoObjetivo, opt => opt.Ignore())
                .ForMember(dest => dest.SeriesObjetivo, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenEjercicio, opt => opt.Ignore())
                .ForMember(dest => dest.NombreEjercicio, opt => opt.Ignore())
                .ForMember(dest => dest.NombreMusculo, opt => opt.Ignore())
                .ForMember(dest => dest.NombreCategoria, opt => opt.Ignore())
                .ForMember(dest => dest.UrlDemoEjercicio, opt => opt.Ignore())
                .ForMember(dest => dest.NombreGrupoMuscular, opt => opt.Ignore());
            CreateMap<EjercicioRegistro, EjercicioRegistroResponse>();

            // Evento Calendario
            CreateMap<EventoCalendario, EventoCalendarioResponse>()
                .ForMember(dest => dest.NombreAlumno, opt => opt.MapFrom(src => src.AlumnoPlan.NombreAlumno));

            // RecordPersonal
            CreateMap<RecordPersonal, RecordPersonalResponse>();
            CreateMap<EjercicioRegistro, RecordPersonal>()
                .ForMember(dest => dest.IdAlumnoPlan, opt => opt.Ignore())
                .ForMember(dest => dest.IdAlumno, opt => opt.Ignore())
                .ForMember(dest => dest.Calculo1RM, opt => opt.Ignore())
                .ForMember(dest => dest.PesoMax, opt => opt.MapFrom(src => src.Peso))
                .ForMember(dest => dest.IdSesionRealizada, opt => opt.MapFrom(src => src.IdSesionRealizada))
                .ForMember(dest => dest.IdEjercicioSesion, opt => opt.MapFrom(src => src.IdEjercicioSesion));

        }
    }
}
