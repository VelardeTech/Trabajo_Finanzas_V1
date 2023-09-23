using System;
using System.Collections.Generic;

namespace Trabajo_Finanzas_V1.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int PrestamoId { get; set; }

    public int NumeroCuota { get; set; }

    public decimal MontoCuota { get; set; }

    public DateOnly FechaPago { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Prestamo Prestamo { get; set; } = null!;
}
