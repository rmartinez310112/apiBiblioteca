using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioBibliotecaController : ControllerBase
    {
        public readonly DbBibliotecaContext _dbcontext;

        public UsuarioBibliotecaController(DbBibliotecaContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<UsuarioBiblioteca> lista = new List<UsuarioBiblioteca>();
            try
            {
                lista = _dbcontext.UsuarioBibliotecas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public IActionResult Obtener(int id)
        {
            UsuarioBiblioteca oUsuario = _dbcontext.UsuarioBibliotecas.Find(id);

            if (oUsuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            try
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oUsuario });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oUsuario });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] UsuarioBiblioteca objeto)
        {
            try
            {
                _dbcontext.UsuarioBibliotecas.Add(objeto);
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
        public IActionResult Editar([FromBody] UsuarioBiblioteca objeto)
        {
            UsuarioBiblioteca? oUsuario = _dbcontext.UsuarioBibliotecas.Find(objeto.Id);
            if (oUsuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            try
            {
                oUsuario.Nombres = objeto.Nombres is null ? oUsuario.Nombres : objeto.Nombres;
                oUsuario.Apellidos = objeto.Apellidos is null ? oUsuario.Apellidos : objeto.Apellidos;

                _dbcontext.UsuarioBibliotecas.Update(oUsuario);
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
            UsuarioBiblioteca? oUsuario = _dbcontext.UsuarioBibliotecas.Find(id);
            if (oUsuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            try
            {

                _dbcontext.UsuarioBibliotecas.Remove(oUsuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpGet]
        [Route("autenticacion/{usua}/{pass}")]
        public IActionResult autenticacion(string usua, string pass)
        {
            Usuario oUsua = _dbcontext.Usuarios.Where(x => x.NombreUsuario == usua && x.Clave == pass).FirstOrDefault();

            if (oUsua == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            try
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oUsua });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, Response = oUsua });
            }
        }
    }
}
