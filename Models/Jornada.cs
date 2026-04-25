using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorBrigadasComunitarias.Models
{
    public class Jornada
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El lugar es obligatorio")]
        [StringLength(150)]
        public string Lugar { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una zona")]
        public int ZonaId { get; set; }

        [ForeignKey("ZonaId")]
        public Zona? Zona { get; set; }

        public ICollection<Participacion> Participaciones { get; set; } = new List<Participacion>();
        public ICollection<Material> Materiales { get; set; } = new List<Material>();
    }
}