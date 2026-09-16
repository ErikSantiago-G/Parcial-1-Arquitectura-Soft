using System.Collections.Generic;
using CapaNegocio.Interfaces;

namespace DatosSimulados
{
    // Implementa el MISMO contrato que la clase real de AccesoDatos.
    public class CentralRiesgoSimulada : IAccesoCentralRiesgo
    {
        private readonly Dictionary<(string tipoDoc, string nroDoc), int> _puntajes;

        public CentralRiesgoSimulada()
        {
            _puntajes = new Dictionary<(string, string), int>
            {
                { ("CC", "12234587"), 563 },
                { ("CC", "1000111222"), 820 },
                { ("CC", "1000333444"), 650 },
                { ("CC", "1000555666"), 410 },
            };
        }



        // Permite que cada prueba registre el puntaje exacto que necesita para ese escenario, sin tocar los datos precargados de arriba.
        public void RegistrarPuntaje(string tipoDoc, string nroDoc, int puntaje)
        {
            _puntajes[(tipoDoc, nroDoc)] = puntaje;
        }




        public int ConsultarPuntaje(string tipoDoc, string nroDoc)
        {
            return _puntajes.TryGetValue((tipoDoc, nroDoc), out int puntaje) 
                ? puntaje : 0;
        }
    }
}