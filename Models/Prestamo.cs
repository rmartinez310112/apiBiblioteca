using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Prestamo
    {
        [Key]
        public int IdPrestamo { get; set; }

        public int? IdUsuarioBiblioteca { get; set; }

        public int? IdLibro { get; set; }

        public DateTime? FechaPrestamo { get; set; }

        public DateTime? FechaDevolucion { get; set; }

        public string? EstadoPrestamo { get; set; }

        public string? mensajeError { get; set; }

        public Usuario? oUsuario { get; set; }
        public Libro? oLibro { get; set; }
    }
}
