using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        public readonly DbBibliotecaContext _dbcontext;

        public LibrosController(DbBibliotecaContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Libro> lista = new List<Libro>();
            try
            {
                lista = _dbcontext.Libros.Include(x => x.oCategoria).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpGet]
        [Route("ObtenerLibro/{id:int}")]
        public IActionResult Obtener(int id)
        {
            Libro oLibro = _dbcontext.Libros.Find(id);

            if (oLibro == null)
            {
                return BadRequest("Libro no encontrado");
            }

            try
            {
                oLibro = _dbcontext.Libros.Include(x => x.oCategoria).Where(p => p.IdLibro == id).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oLibro });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oLibro });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Libro objeto)
        {
            try
            {
                _dbcontext.Libros.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Libro objeto)
        {
            Libro? oLibro = _dbcontext.Libros.Find(objeto.IdLibro);
            if (oLibro == null)
            {
                return BadRequest("Libro no encontrado");
            }

            try
            {
                oLibro.IdCategoria = objeto.IdCategoria is null ? oLibro.IdCategoria : objeto.IdCategoria;
                oLibro.Titulo = objeto.Titulo is null ? oLibro.Titulo : objeto.Titulo;
                oLibro.Autor = objeto.Autor is null ? oLibro.Autor : objeto.Autor;
                oLibro.FechaPublicacion = objeto.FechaPublicacion is null ? oLibro.FechaPublicacion : objeto.FechaPublicacion;
                oLibro.Cantidad = objeto.Cantidad is null ? oLibro.Cantidad : objeto.Cantidad;

                _dbcontext.Libros.Update(oLibro);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            Libro? oLibro = _dbcontext.Libros.Find(id);
            if (oLibro == null)
            {
                return BadRequest("Libro no encontrado");
            }
            try
            {

                _dbcontext.Libros.Remove(oLibro);
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
