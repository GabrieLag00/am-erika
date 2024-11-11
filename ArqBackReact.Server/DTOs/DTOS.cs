namespace ArqBackReact.Server.DTOs
{
    public class ColaboradorDto
    {
        public string Nombre { get; set; } = null!;
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsProfesor { get; set; }
        public ProfesorDto? Profesor { get; set; }
        public AdministrativoDto? Administrativo { get; set; }
    }

    public class ProfesorDto
    {
        public string Correo { get; set; } = null!;
        public string Departamento { get; set; } = null!;
    }

    public class AdministrativoDto
    {
        public string Correo { get; set; } = null!;
        public string Puesto { get; set; } = null!;
        public decimal Nomina { get; set; }
    }
}
