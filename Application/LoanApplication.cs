using ApiBiblio.Context;
using ApiBiblio.Interfaces;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Application
{
    public class LoanApplication : ILoanApplication
    {
        public async Task<List<Prestamo>> GetPrestamos()
        {
            using (var context = new ApiContext())
            {
                var lista = context.Prestamos.ToList();
                return lista;
            }
        }

        public async Task<int> AgregarPrestamo(Prestamo prestamo)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    Libro? oLibro = context.Libros.Where(x => x.IdLibro == prestamo.IdLibro).FirstOrDefault();

                    if (oLibro == null)
                    {
                        return 100;
                    }
                    else if (oLibro.Cantidad < 1)
                    {
                        return 0;
                    }

                    EditarCantidadLibros(oLibro, 0);


                    var siguienteId = context.Prestamos.Count() > 0 ? context.Prestamos.Max(p => p.IdPrestamo) : 0;

                    context.Prestamos.Add(new Prestamo
                    {
                        IdPrestamo = siguienteId + 1,
                        IdUsuarioBiblioteca = prestamo.IdUsuarioBiblioteca,
                        IdLibro = prestamo.IdLibro,
                        FechaPrestamo = DateTime.Now,
                        FechaDevolucion = null,
                        EstadoPrestamo="Prestado"
                    });

                    context.SaveChanges();

                    return context.Prestamos.LastOrDefault(p => p != null).IdPrestamo;

                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error al Agregar prestamo: {ex.Message}");
            }
        }

        
        
        
        
        private void EditarCantidadLibros(Libro obj, float aumenta)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    Libro? oLibro = context.Libros.Find(obj.IdLibro);
                    if (oLibro != null) {
                        
                        oLibro.Cantidad = aumenta == 1 ? (obj.Cantidad + 1) : (obj.Cantidad - 1);

                        context.Libros.Update(oLibro);
                        context.SaveChanges();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> DevolverPrestamo(int prestamoId)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    Prestamo? oPrestamo = context.Prestamos.Find(prestamoId);
                    Libro? oLibro = context.Libros.Where(x => x.IdLibro == oPrestamo.IdLibro).FirstOrDefault();

                    if (oPrestamo == null)
                    {
                        return 100;
                    }

                    if (oLibro == null)
                    {
                        return 0;
                    }

                    EditarCantidadLibros(oLibro, 1);

                    oPrestamo.FechaDevolucion = DateTime.Now;
                    oPrestamo.EstadoPrestamo = "Devuelto";


                    context.Prestamos.Update(oPrestamo);
                    context.SaveChanges();

                    return oPrestamo.IdPrestamo;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
