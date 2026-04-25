using System.ComponentModel.DataAnnotations;

namespace GestorBrigadasComunitarias.Models
{
    public class Zona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la zona es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        public ICollection<Jornada> Jornadas { get; set; } = new List<Jornada>();
    }
}