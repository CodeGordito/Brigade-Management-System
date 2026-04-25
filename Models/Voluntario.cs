using System.ComponentModel.DataAnnotations;

namespace GestorBrigadasComunitarias.Models
{
    public class Voluntario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Teléfono no válido")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string? Correo { get; set; }

        public ICollection<Participacion> Participaciones { get; set; } = new List<Participacion>();
        public string NombreCompleto => Nombre + " " + Apellido;
    }
}