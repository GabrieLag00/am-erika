namespace ArqBackReact.Server.Models
{
    public class Profesor
    {
        public int IdProfesor { get; set; }
        public int FKColaborador { get; set; }
        public string Correo { get; set; } = null!;
        public string Departamento { get; set; } = null!;

        // Navegación hacia Colaborador
        public Colaborador Colaborador { get; set; }
    }
}
