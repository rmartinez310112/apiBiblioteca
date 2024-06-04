using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiBiblioteca.Models;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public int? IdUsuarioBiblioteca { get; set; }

    public int? IdLibro { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public string? EstadoPrestamo { get; set; }

    [JsonIgnore]
    public virtual Libro? oLibro { get; set; }
    [JsonIgnore]
    public virtual UsuarioBiblioteca? oUsuarioBiblioteca { get; set; }
}
