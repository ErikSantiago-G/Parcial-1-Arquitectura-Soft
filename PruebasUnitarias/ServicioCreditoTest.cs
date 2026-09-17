using CapaNegocio.Modelos;
using CapaNegocio.Servicios;
using DatosSimulados;
using Xunit;

namespace PruebasUnitarias
{
    public class ServicioCreditoTest
    {
        private const string TIPO_DOC = "CC";
        private const string NRO_DOC = "123";

        // Metodo auxiliar: arma un ServicioCredito real, pero con
        // CentralRiesgoSimulada en vez de CapaAccesoDatos. Asi las pruebas
        // no tocan SQL Server.
        private ServicioCredito CrearServicio(int? puntajeRegistrado = null)
        {
            var simulada = new CentralRiesgoSimulada();
            if (puntajeRegistrado.HasValue)
            {
                simulada.RegistrarPuntaje(TIPO_DOC, NRO_DOC, puntajeRegistrado.Value);
            }
            return new ServicioCredito(simulada);
        }

        private SolicitudCredito Solicitud(decimal monto, int plazo, decimal ingresos = 3000, decimal egresos = 2000)
        {
            return new SolicitudCredito
            {
                TipoDoc = TIPO_DOC,
                NroDoc = NRO_DOC,
                IngresosTotales = ingresos,
                EgresosTotales = egresos,
                MontoSolicitado = monto,
                PlazoSolicitado = plazo
            };
        }

        [Theory]
        [InlineData(0)]
        [InlineData(73)]
        public void Plazo_fuera_de_1_a_72_meses_se_rechaza(int plazoInvalido)
        {
            var servicio = CrearServicio();
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 5000, plazo: plazoInvalido));

            Assert.False(resultado.Aprobado);
        }

        [Fact]
        public void Balanza_negativa_se_rechaza()
        {
            var servicio = CrearServicio();
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 5000, plazo: 10, ingresos: 1000, egresos: 2000));

            Assert.False(resultado.Aprobado);
        }

        [Fact]
        public void Balanza_cero_se_rechaza()
        {
            var servicio = CrearServicio();
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 5000, plazo: 10, ingresos: 2000, egresos: 2000));

            Assert.False(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_mayor_o_igual_a_0_95_se_rechaza_sin_consultar_puntaje()
        {
            var servicio = CrearServicio(); // sin puntaje registrado
            // balanza = 1000, relacion = (9500/10)/1000 = 0.95
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 9500, plazo: 10));

            Assert.False(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_alta_0_7_a_0_95_con_puntaje_suficiente_se_aprueba()
        {
            // relacion = (8000/10)/1000 = 0.8 => exige puntaje >= 800
            var servicio = CrearServicio(puntajeRegistrado: 800);
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 8000, plazo: 10));

            Assert.True(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_alta_0_7_a_0_95_con_puntaje_insuficiente_se_rechaza()
        {
            var servicio = CrearServicio(puntajeRegistrado: 799);
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 8000, plazo: 10));

            Assert.False(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_media_0_4_a_0_7_con_puntaje_suficiente_se_aprueba()
        {
            // relacion = (5000/10)/1000 = 0.5 => exige puntaje >= 600
            var servicio = CrearServicio(puntajeRegistrado: 600);
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 5000, plazo: 10));

            Assert.True(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_media_0_4_a_0_7_con_puntaje_insuficiente_se_rechaza()
        {
            var servicio = CrearServicio(puntajeRegistrado: 599);
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 5000, plazo: 10));

            Assert.False(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_baja_menor_a_0_4_con_puntaje_suficiente_se_aprueba()
        {
            // relacion = (2000/10)/1000 = 0.2 => exige puntaje >= 400
            var servicio = CrearServicio(puntajeRegistrado: 400);
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 2000, plazo: 10));

            Assert.True(resultado.Aprobado);
        }

        [Fact]
        public void Relacion_baja_menor_a_0_4_con_puntaje_insuficiente_se_rechaza()
        {
            var servicio = CrearServicio(puntajeRegistrado: 399);
            var resultado = servicio.EvaluarCredito(Solicitud(monto: 2000, plazo: 10));

            Assert.False(resultado.Aprobado);
        }
    }
}