using ApiBiblio.Interfaces;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        readonly IBookApplication _ibookApplication;
        public BookController(IBookApplication ibookApplication) { 
         _ibookApplication=ibookApplication;
        }

        [HttpGet]
        [Route("Lista")]
        [ProducesResponseType(typeof(List<Libro>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Libro>>> ObtenerLibros()
        {
           return Ok(await _ibookApplication.GetLibros());
        }

        [HttpPost, Route("Guardar")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> AgregarLibro(Libro libro)
        {
            try
            {
                var response = await _ibookApplication.AgregarLibro(libro);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut, Route("Editar")]
        [ProducesResponseType(typeof(Libro), StatusCodes.Status200OK)]
        public IActionResult Editar([FromBody] Libro objeto)
        {
            try
            {
                var response = _ibookApplication.ActualizarLibro(objeto);

                if (response == null)
                {
                    return BadRequest("No existe ese libro");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpDelete, Route("Eliminar/{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            try
            {
                var response = await _ibookApplication.EliminarLibro(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }
    }
}
