using System;
using System.Collections.Generic;

namespace Trabajo_Finanzas_V1.Models;

public partial class OfertasVehiculare
{
    public int Id { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public decimal Precio { get; set; }

    public decimal Van { get; set; }

    public decimal Tir { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
