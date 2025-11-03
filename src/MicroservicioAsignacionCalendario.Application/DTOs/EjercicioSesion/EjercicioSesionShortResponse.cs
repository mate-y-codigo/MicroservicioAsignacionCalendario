using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class EjercicioSesionShortResponse
    {

        // TO DO: Revisar SHORT RESPONSE
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid EjercicioId { get; set; }
        //[Required]
        //public string Nombre { get; set; }
    }
}
