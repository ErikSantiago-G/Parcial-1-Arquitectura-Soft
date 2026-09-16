using System;
using CapaAccesoDatos;
using CapaNegocio.Interfaces;
using CapaNegocio.Servicios;

namespace CapaPresentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Aqui se conectan las implementaciones concretas
            // con las abstracciones que pide Negocio.
            IAccesoCentralRiesgo accesoCentralRiesgo = new CapaAccesoDatos.CapaAccesoDatos();
            IServicioCredito servicioCredito = new ServicioCredito(accesoCentralRiesgo);

            Application.Run(new Form1(servicioCredito));
        }
    }
}