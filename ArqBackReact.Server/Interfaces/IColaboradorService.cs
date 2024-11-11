using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArqBackReact.Server.Models;

namespace ArqBackReact.Server.Services
{
    public interface IColaboradorService
    {
        Task<IEnumerable<Colaborador>> GetColaboradoresAsync(DateTime? fechaInicio, DateTime? fechaFin, string tipo);

        Task<Colaborador> AddColaboradorAsync(ColaboradorRequest colaboradorRequest);

        Task<bool> DeleteColaboradorAsync(int idColaborador);
    }
}
