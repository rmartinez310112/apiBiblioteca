using ApiBiblio.Context;
using ApiBiblio.Interfaces;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders.Physical;

namespace ApiBiblio.Application
{
    public class bookApplication : IBookApplication
    {
        
        public async Task<List<Libro>> GetLibros()
        {
            using (var context = new ApiContext())
            {
                var lista = context.Libros.ToList();
                return lista;
            }
            
        }

        public async Task<int> AgregarLibro(Libro libro)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    var siguienteId = context.Libros.Count() > 0 ? context.Libros.Max(p => p.IdLibro) : 0;

                     context.Libros.Add(new Libro
                    {
                        IdLibro = siguienteId + 1,
                        IdCategoria = libro.IdCategoria,
                        Titulo = libro.Titulo,
                        Autor = libro.Autor,
                        FechaPublicacion = libro.FechaPublicacion,
                        Cantidad = libro.Cantidad,
                        FechaCreacion = DateTime.Now
                    });

                     context.SaveChanges();

                    return context.Libros.LastOrDefault(p => p != null).IdLibro;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al Agregar Libro: {ex.Message}");
            }
        }

        public async Task<Libro> ActualizarLibro(Libro libro)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    Libro? oLibro = context.Libros.Find(libro.IdLibro);

                    if (oLibro == null)
                    {
                        return null;
                    }

                    oLibro.IdCategoria = libro.IdCategoria == 0 ? oLibro.IdCategoria : libro.IdCategoria;
                    oLibro.Titulo = libro.Titulo is null ? oLibro.Titulo : libro.Titulo;
                    oLibro.Autor = libro.Autor is null ? oLibro.Autor : libro.Autor;
                    oLibro.FechaPublicacion = libro.FechaPublicacion is null ? oLibro.FechaPublicacion : libro.FechaPublicacion;
                    oLibro.Cantidad = libro.Cantidad == 0 ? oLibro.Cantidad : libro.Cantidad;

                    return context.Libros.FirstOrDefault(p => p.IdLibro == libro.IdLibro);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al actualizar libro: {ex.Message}");
            }
        }

        public async Task<bool> EliminarLibro(int libroId)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    Libro? oLibro = context.Libros.Find(libroId);

                    if (oLibro == null)
                    {
                        return false;
                    }

                    context.Libros.Remove(oLibro);
                    context.SaveChanges();

                    return true;
                }
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar libro: {ex.Message}");
            }
        }
    }
}
