using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Direccion { get; set; }

    public int? Venta { get; set; }

    public int? IdZona { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }

    public string Descripcion { get; set; }
}
