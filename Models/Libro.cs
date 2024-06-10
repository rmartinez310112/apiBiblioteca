using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBiblio.Models
{
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }
        
        public int IdCategoria { get; set; }

        public string? Titulo { get; set; }

        public string? Autor { get; set; }

        public DateOnly? FechaPublicacion { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Categoria? oCategoria { get; set; }
    }
}
