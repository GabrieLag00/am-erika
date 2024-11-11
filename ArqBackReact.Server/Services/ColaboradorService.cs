using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArqBackReact.Server.Models;
using ArqBackReact.Server.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ArqBackReact.Server.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly NetdbContext _context;

        public ColaboradorService(NetdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Colaborador>> GetColaboradoresAsync(DateTime? fechaInicio, DateTime? fechaFin, string tipo)
        {
            var query = _context.Colaboradores.AsQueryable();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(c => c.FechaCreacion >= fechaInicio.Value && c.FechaCreacion <= fechaFin.Value);
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                if (tipo == "profesor")
                {
                    query = query.Where(c => c.IsProfesor);
                }
                else if (tipo == "administrativo")
                {
                    query = query.Where(c => !c.IsProfesor);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<Colaborador> AddColaboradorAsync(ColaboradorRequest colaboradorRequest)
        {
            var colaborador = new Colaborador
            {
                Nombre = colaboradorRequest.Nombre,
                Edad = colaboradorRequest.Edad,
                Birthday = colaboradorRequest.Birthday,
                IsProfesor = colaboradorRequest.IsProfesor,
                FechaCreacion = colaboradorRequest.FechaCreacion
            };

            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();

            if (colaboradorRequest.IsProfesor)
            {
                var profesor = new Profesor
                {
                    FKColaborador = colaborador.IdColaborador,
                    Correo = colaboradorRequest.Correo,
                    Departamento = colaboradorRequest.Departamento
                };
                _context.Profesores.Add(profesor);
            }
            else
            {
                var administrativo = new Administrativo
                {
                    FKColaborador = colaborador.IdColaborador,
                    Correo = colaboradorRequest.Correo,
                    Puesto = colaboradorRequest.Puesto,
                    Nomina = colaboradorRequest.Nomina.Value
                };
                _context.Administrativos.Add(administrativo);
            }

            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<bool> DeleteColaboradorAsync(int idColaborador)
        {
            var colaborador = await _context.Colaboradores
                .Include(c => c.Profesor)
                .Include(c => c.Administrativo)
                .FirstOrDefaultAsync(c => c.IdColaborador == idColaborador);
            if (colaborador == null) { return false; }
            _context.Colaboradores.Remove(colaborador); await _context.SaveChangesAsync(); return true;

        }
    }
}

