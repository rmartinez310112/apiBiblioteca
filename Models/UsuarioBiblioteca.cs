using System;
using System.Collections.Generic;

namespace ApiBiblioteca.Models;

public partial class UsuarioBiblioteca
{
    public int Id { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
