using System;
using System.Collections.Generic;

namespace ArqBackReact.Server.Models
{
    public class Colaborador
    {
        public int IdColaborador { get; set; }
        public string Nombre { get; set; } = null!;
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsProfesor { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relaciones de navegación
        public Profesor? Profesor { get; set; }
        public Administrativo? Administrativo { get; set; }
    }

    public class ColaboradorRequest
    {

        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsProfesor { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Correo { get; set; }
        public string Departamento { get; set; } // Usado solo si es profesor
        public string Puesto { get; set; }       // Usado solo si es administrativo
        public decimal? Nomina { get; set; }     // Usado solo si es administrativo

    }
}
