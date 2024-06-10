using ApiBiblio.Models;

namespace ApiBiblio.Interfaces
{
    public interface IBookApplication
    {
        public Task<List<Libro>> GetLibros();
        public Task<int> AgregarLibro(Libro libro);
        public Task<Libro> ActualizarLibro(Libro libro);
        public Task<bool> EliminarLibro(int libroId);
    }
}
