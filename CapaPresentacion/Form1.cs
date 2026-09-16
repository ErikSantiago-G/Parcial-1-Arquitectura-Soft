using System;
using System.Windows.Forms;
using CapaNegocio.Interfaces;
using CapaNegocio.Modelos;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private readonly IServicioCredito _servicioCredito;

        public Form1(IServicioCredito servicioCredito)
        {
            InitializeComponent();
            _servicioCredito = servicioCredito;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SolicitudCredito solicitud;

            try
            {
                solicitud = new SolicitudCredito
                {
                    TipoDoc = txtTipoDoc.Text.Trim(),
                    NroDoc = txtNroDoc.Text.Trim(),
                    IngresosTotales = decimal.Parse(txtIngresosTotales.Text.Trim()),
                    EgresosTotales = decimal.Parse(txtEgresosTotales.Text.Trim()),
                    MontoSolicitado = decimal.Parse(txtMontoSolicitado.Text.Trim()),
                    PlazoSolicitado = int.Parse(txtPlazoSolicitado.Text.Trim())
                };
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Revise que Ingresos, Egresos, Monto y Plazo sean numericos.",
                    "Datos invalidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ResultadoCredito resultado = _servicioCredito.EvaluarCredito(solicitud);

            MostrarAlert(resultado);
        }

        private void MostrarAlert(ResultadoCredito resultado)
        {
            string titulo = resultado.Aprobado ? "Credito Aprobado" : "Credito Rechazado";
            MessageBoxIcon icono = resultado.Aprobado ? MessageBoxIcon.Information : MessageBoxIcon.Error;

            MessageBox.Show(resultado.Mensaje, titulo, MessageBoxButtons.OK, icono);
        }
    }
}