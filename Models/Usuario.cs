using System;
using System.Collections.Generic;

namespace ApiBiblioteca.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Clave { get; set; }

    public DateTime? FechaCreacion { get; set; }
}
