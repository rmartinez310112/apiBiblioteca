using ApiBiblio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "dbBiblioteca");
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }


    }
}
