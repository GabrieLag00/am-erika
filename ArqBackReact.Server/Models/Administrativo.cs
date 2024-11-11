namespace ArqBackReact.Server.Models
{
    public class Administrativo
    {
        public int IdAdministrativo { get; set; }
        public int FKColaborador { get; set; }
        public string Correo { get; set; } = null!;
        public string Puesto { get; set; } = null!;
        public decimal Nomina { get; set; }

        // Navegación hacia Colaborador
        public Colaborador Colaborador { get; set; }
    }
}
