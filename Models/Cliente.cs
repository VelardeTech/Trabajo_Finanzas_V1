using System;
using System.Collections.Generic;

namespace Trabajo_Finanzas_V1.Models;

public partial class Cliente
{
    public int IdUser { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public virtual ICollection<Configuracione> Configuraciones { get; set; } = new List<Configuracione>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
