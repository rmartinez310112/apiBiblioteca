using System;
using System.Collections.Generic;

namespace ApiBiblioteca.Models;

public partial class Libro
{
    public int IdLibro { get; set; }

    public int? IdCategoria { get; set; }

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public DateOnly? FechaPublicacion { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Categoria? oCategoria { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
