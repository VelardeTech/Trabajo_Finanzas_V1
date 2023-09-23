using System;
using System.Collections.Generic;

namespace Trabajo_Finanzas_V1.Models;

public partial class Configuracione
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string Moneda { get; set; } = null!;

    public string TipoTasa { get; set; } = null!;

    public int? PlazoGracia { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
