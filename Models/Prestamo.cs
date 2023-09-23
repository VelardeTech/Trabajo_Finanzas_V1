using System;
using System.Collections.Generic;

namespace Trabajo_Finanzas_V1.Models;

public partial class Prestamo
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int OfertaVehicularId { get; set; }

    public string Moneda { get; set; } = null!;

    public decimal TasaInteres { get; set; }

    public int Plazo { get; set; }

    public int? PeriodoGracia { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual OfertasVehiculare OfertaVehicular { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
