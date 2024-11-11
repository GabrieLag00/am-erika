using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArqBackReact.Server.DTOs;
using ArqBackReact.Server.Models;
using ArqBackReact.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArqBackReact.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradoresController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetColaboradores(DateTime? fechaInicio, DateTime? fechaFin, string tipo)
        {
            var colaboradores = await _colaboradorService.GetColaboradoresAsync(fechaInicio, fechaFin, tipo);

            // Proyección para aplanar la estructura
            var result = colaboradores.Select(c => new
            {
                c.IdColaborador,
                c.Nombre,
                c.Edad,
                c.Birthday,
                c.IsProfesor,
                c.FechaCreacion,
                Profesor = c.IsProfesor ? new
                {
                    c.Profesor?.IdProfesor,
                    c.Profesor?.Correo,
                    c.Profesor?.Departamento
                } : null,
                Administrativo = !c.IsProfesor ? new
                {
                    c.Administrativo?.IdAdministrativo,
                    c.Administrativo?.Correo,
                    c.Administrativo?.Puesto,
                    c.Administrativo?.Nomina
                } : null
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Colaborador>> AddColaborador([FromBody] ColaboradorRequest colaboradorRequest)
        {
            var nuevoColaborador = await _colaboradorService.AddColaboradorAsync(colaboradorRequest);

            var response = new
            {
                nuevoColaborador.IdColaborador,
                nuevoColaborador.Nombre,
                nuevoColaborador.Edad,
                nuevoColaborador.Birthday,
                nuevoColaborador.IsProfesor,
                nuevoColaborador.FechaCreacion,
                Profesor = nuevoColaborador.IsProfesor ? nuevoColaborador.Profesor != null ? new
                {
                    nuevoColaborador.Profesor.IdProfesor,
                    nuevoColaborador.Profesor.Correo,
                    nuevoColaborador.Profesor.Departamento
                } : null : null,
                Administrativo = !nuevoColaborador.IsProfesor ? nuevoColaborador.Administrativo != null ? new
                {
                    nuevoColaborador.Administrativo.IdAdministrativo,
                    nuevoColaborador.Administrativo.Correo,
                    nuevoColaborador.Administrativo.Puesto,
                    nuevoColaborador.Administrativo.Nomina
                } : null : null
            };

            return CreatedAtAction(nameof(AddColaborador), new { id = nuevoColaborador.IdColaborador }, response);
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteColaborador(int id) 
        { 
            var result = await _colaboradorService.DeleteColaboradorAsync(id); if (!result) { return NotFound(); } return NoContent(); 
        }
    }
}
