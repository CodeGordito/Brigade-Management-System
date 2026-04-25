using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorBrigadasComunitarias.Models
{
    public class Participacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un voluntario")]
        public int VoluntarioId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una jornada")]
        public int JornadaId { get; set; }

        public bool Asistio { get; set; }

        [Range(0, 24, ErrorMessage = "Las horas deben estar entre 0 y 24")]
        public int HorasTrabajadas { get; set; }

        [StringLength(250)]
        public string? Observaciones { get; set; }

        [ForeignKey("VoluntarioId")]
        public Voluntario? Voluntario { get; set; }

        [ForeignKey("JornadaId")]
        public Jornada? Jornada { get; set; }
    }
}