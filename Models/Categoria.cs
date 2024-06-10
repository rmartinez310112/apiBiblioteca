using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        public string? Nombre { get; set; }
    }
}
