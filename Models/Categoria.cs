using System;
using System.Collections.Generic;

namespace ApiBiblioteca.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
