using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IAccesoCentralRiesgo
    {
        int ConsultarPuntaje(string tipoDoc, string nroDoc);
    }
}
