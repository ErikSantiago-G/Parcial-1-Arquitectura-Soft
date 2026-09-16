using CapaNegocio.Interfaces;
using CapaNegocio.Modelos;

namespace CapaNegocio.Servicios
{
    public class ServicioCredito : IServicioCredito
    {
        private readonly IAccesoCentralRiesgo _accesoCentralRiesgo;

        public ServicioCredito(IAccesoCentralRiesgo accesoCentralRiesgo)
        {
            _accesoCentralRiesgo = accesoCentralRiesgo;
        }



        public ResultadoCredito EvaluarCredito(SolicitudCredito solicitud)
        {
            // Regla 1: el plazo debe estar entre 1 y 72 meses
            if (!PlazoEsValido(solicitud.PlazoSolicitado))
            {
                return new ResultadoCredito
                {
                    Aprobado = false,
                    Mensaje = "El plazo solicitado debe estar entre 1 y 72 meses."
                };
            }



            // Regla 2: balanza = ingresos - egresos
            decimal balanza = CalcularBalanza(solicitud.IngresosTotales, solicitud.EgresosTotales);



            // Regla 3: si la balanza es cero o negativa, se rechaza
            if (balanza <= 0)
            {
                return new ResultadoCredito
                {
                    Aprobado = false,
                    Mensaje = "La balanza (ingresos - egresos) es cero o negativa.",
                    Balance = balanza
                };
            }




            // Regla 4: relacionCreditoBalanza = (Monto / Plazo) / Balanza
            decimal relacion = CalcularRelacionCreditoBalanza(
                solicitud.MontoSolicitado, solicitud.PlazoSolicitado, balanza);




            // Regla 5: si la relacion es >= 0.95, se rechaza SIN consultar puntaje
            if (relacion >= 0.95m)
            {
                return new ResultadoCredito
                {
                    Aprobado = false,
                    Mensaje = "La relacion credito/balanza es igual o superior a 0.95.",
                    Balance = balanza,
                    RelacionCreditoBalance = relacion
                };
            }



            // Reglas 6,7,8 segun el tramo de relacion, se exige un puntaje minimo
            int puntajeMinimoRequerido = ObtenerPuntajeMinimoRequerido(relacion);

            int puntaje = _accesoCentralRiesgo.ConsultarPuntaje(solicitud.TipoDoc, solicitud.NroDoc);

            bool aprobado = puntaje >= puntajeMinimoRequerido;

            return new ResultadoCredito
            {
                Aprobado = aprobado,
                Mensaje = aprobado
                    ? $"Puntaje {puntaje} cumple el minimo de {puntajeMinimoRequerido} requerido."
                    : $"Puntaje {puntaje} no alcanza el minimo de {puntajeMinimoRequerido} requerido.",
                Puntaje = puntaje,
                Balance = balanza,
                RelacionCreditoBalance = relacion
            };
        }



        // --- Metodos privados: cada uno resuelve UNA sola regla Separarlos asi  hace que cada regla se
        // pueda leer, entender y probar de forma independiente.

        private bool PlazoEsValido(int plazoMeses)
        {
            return plazoMeses >= 1 && plazoMeses <= 72;
        }

        private decimal CalcularBalanza(decimal ingresosTotales, decimal egresosTotales)
        {
            return ingresosTotales - egresosTotales;
        }

        private decimal CalcularRelacionCreditoBalanza(decimal montoSolicitado, int plazoSolicitado, decimal balanza)
        {
            decimal cuotaMensual = montoSolicitado / plazoSolicitado;
            return cuotaMensual / balanza;
        }

        private int ObtenerPuntajeMinimoRequerido(decimal relacion)
        {
            if (relacion >= 0.7m) return 800;
            if (relacion >= 0.4m) return 600;
            return 400;
        }
    }
}