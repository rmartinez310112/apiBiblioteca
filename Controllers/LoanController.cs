using ApiBiblio.Application;
using ApiBiblio.Interfaces;
using ApiBiblio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        public readonly ILoanApplication _loanApplication;
        public LoanController(ILoanApplication loanApplication)
        {
            _loanApplication = loanApplication;
        }

        [HttpGet, Route("Lista")]
        [ProducesResponseType(typeof(List<Prestamo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Prestamo>>> ObtenerPrestamos()
        {
            return Ok(await _loanApplication.GetPrestamos());
        }

        [HttpPost, Route("Guardar")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> AgregarPrestamo(Prestamo prestamo)
        {
            try
            {
                var response = await _loanApplication.AgregarPrestamo(prestamo);
                
                if (response == 100)
                {
                   return BadRequest("Libro no encontrado");
                }
                else if (response == 0)
                {
                   return BadRequest("El libro no esta disponible");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut, Route("Devolver/{id:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Devolver(int id)
        {
            try
            {
                var response = await _loanApplication.DevolverPrestamo(id);

                if (response == 100)
                {
                    return BadRequest("Prestamo no encontrado");
                }
                else if (response == 0) {
                    return BadRequest("Libro no encontrado");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }
    }
}
