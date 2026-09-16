using CapaNegocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IServicioCredito
    {
        ResultadoCredito EvaluarCredito(SolicitudCredito solicitud);
    }
}