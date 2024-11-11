using System;
using System.Collections.Generic;

namespace ArqBackReact.Server.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string Correo { get; set; } = null!;
}
