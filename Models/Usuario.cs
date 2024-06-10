using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public DateTime? FechaCreacion { get; set; }
    }
}
