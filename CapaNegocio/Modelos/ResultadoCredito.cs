using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Modelos
{
    public class ResultadoCredito
    {
        public bool Aprobado { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int Puntaje { get; set; }
        public decimal Balance { get; set; }
        public decimal RelacionCreditoBalance { get; set; }
    }
}
