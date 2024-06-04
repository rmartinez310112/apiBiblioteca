using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        public readonly DbBibliotecaContext _dbcontext;

        public PrestamoController(DbBibliotecaContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Prestamo> lista = new List<Prestamo>();
            try
            {
                lista = _dbcontext.Prestamos.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpGet]
        [Route("ObtenerPrestamo/{id:int}")]
        public IActionResult Obtener(int id)
        {
            Prestamo oPrestamo = _dbcontext.Prestamos.Find(id);

            if (oPrestamo == null)
            {
                return BadRequest("Prestamo no encontrado");
            }

            try
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oPrestamo });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oPrestamo });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Prestamo objeto)
        {
            Libro? oLibro = _dbcontext.Libros.Where(x => x.IdLibro == objeto.IdLibro).FirstOrDefault();

            if (oLibro == null)
            {
                return BadRequest("Libro no encontrado");
            }
            else if (oLibro.Cantidad < 1)
            {
                return BadRequest("El libro no esta disponible");
            }


            try
            {
                EditarCantidadLibros(oLibro, 0);

                objeto.FechaDevolucion = DateTime.Now;
                objeto.EstadoPrestamo = "Prestado";

                _dbcontext.Prestamos.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        private void EditarCantidadLibros(Libro obj, float aumenta)
        {
            Libro? oLibro = _dbcontext.Libros.Find(obj.IdLibro);

            try
            {
                oLibro.Cantidad = aumenta == 1 ? (obj.Cantidad + 1) : (obj.Cantidad - 1);


                _dbcontext.Libros.Update(oLibro);
                _dbcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPut]
        [Route("Devolver/{id:int}")]
        public IActionResult Editar(int id)
        {
            Prestamo? oPrestamo = _dbcontext.Prestamos.Find(id);
            Libro? oLibro = _dbcontext.Libros.Where(x => x.IdLibro == oPrestamo.IdLibro).FirstOrDefault();

            if (oPrestamo == null)
            {
                return BadRequest("Prestamo no encontrado");
            }

            if (oLibro == null)
            {
                return BadRequest("Libro no encontrado");
            }

            try
            {
                EditarCantidadLibros(oLibro, 1);

                oPrestamo.FechaDevolucion = DateTime.Now;
                oPrestamo.EstadoPrestamo = "Devuelto";
                

                _dbcontext.Prestamos.Update(oPrestamo);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        
    }
}
