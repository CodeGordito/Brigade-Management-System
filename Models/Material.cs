using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorBrigadasComunitarias.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del material es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, 10000, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public int Cantidad { get; set; }

        [StringLength(50)]
        public string? UnidadMedida { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una jornada")]
        public int JornadaId { get; set; }

        [ForeignKey("JornadaId")]
        public Jornada? Jornada { get; set; }
    }
}