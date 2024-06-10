using ApiBiblio.Models;

namespace ApiBiblio.Interfaces
{
    public interface ILoanApplication
    {
        public Task<List<Prestamo>> GetPrestamos();
        public Task<int> AgregarPrestamo(Prestamo prestamo);
        public Task<int> DevolverPrestamo(int prestamoId);
    }
}
