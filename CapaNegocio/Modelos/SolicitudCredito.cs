using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Modelos 
{ 
public class SolicitudCredito
{
    public string TipoDoc { get; set; } = string.Empty;
    public string NroDoc { get; set; } = string.Empty;
    public decimal IngresosTotales { get; set; }
    public decimal EgresosTotales { get; set; }
    public decimal MontoSolicitado { get; set; }
    public int PlazoSolicitado { get; set; }
}
}
