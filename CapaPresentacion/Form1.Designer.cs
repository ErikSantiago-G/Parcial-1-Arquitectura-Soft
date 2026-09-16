namespace CapaPresentacion
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.TextBox txtTipoDoc;
        private System.Windows.Forms.TextBox txtNroDoc;
        private System.Windows.Forms.TextBox txtIngresosTotales;
        private System.Windows.Forms.TextBox txtEgresosTotales;
        private System.Windows.Forms.TextBox txtMontoSolicitado;
        private System.Windows.Forms.TextBox txtPlazoSolicitado;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lblTipoDoc;
        private System.Windows.Forms.Label lblNroDoc;
        private System.Windows.Forms.Label lblIngresosTotales;
        private System.Windows.Forms.Label lblEgresosTotales;
        private System.Windows.Forms.Label lblMontoSolicitado;
        private System.Windows.Forms.Label lblPlazoSolicitado;

        private void InitializeComponent()
        {
            this.txtTipoDoc = new System.Windows.Forms.TextBox();
            this.txtNroDoc = new System.Windows.Forms.TextBox();
            this.txtIngresosTotales = new System.Windows.Forms.TextBox();
            this.txtEgresosTotales = new System.Windows.Forms.TextBox();
            this.txtMontoSolicitado = new System.Windows.Forms.TextBox();
            this.txtPlazoSolicitado = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.lblTipoDoc = new System.Windows.Forms.Label();
            this.lblNroDoc = new System.Windows.Forms.Label();
            this.lblIngresosTotales = new System.Windows.Forms.Label();
            this.lblEgresosTotales = new System.Windows.Forms.Label();
            this.lblMontoSolicitado = new System.Windows.Forms.Label();
            this.lblPlazoSolicitado = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblTipoDoc.Text = "Tipo Doc (ej. CC)";
            this.lblTipoDoc.Location = new System.Drawing.Point(20, 20);
            this.lblTipoDoc.Size = new System.Drawing.Size(150, 20);
            this.txtTipoDoc.Location = new System.Drawing.Point(190, 20);
            this.txtTipoDoc.Size = new System.Drawing.Size(180, 20);

            this.lblNroDoc.Text = "Nro Doc";
            this.lblNroDoc.Location = new System.Drawing.Point(20, 55);
            this.lblNroDoc.Size = new System.Drawing.Size(150, 20);
            this.txtNroDoc.Location = new System.Drawing.Point(190, 55);
            this.txtNroDoc.Size = new System.Drawing.Size(180, 20);

            this.lblIngresosTotales.Text = "Ingresos Totales";
            this.lblIngresosTotales.Location = new System.Drawing.Point(20, 90);
            this.lblIngresosTotales.Size = new System.Drawing.Size(150, 20);
            this.txtIngresosTotales.Location = new System.Drawing.Point(190, 90);
            this.txtIngresosTotales.Size = new System.Drawing.Size(180, 20);

            this.lblEgresosTotales.Text = "Egresos Totales";
            this.lblEgresosTotales.Location = new System.Drawing.Point(20, 125);
            this.lblEgresosTotales.Size = new System.Drawing.Size(150, 20);
            this.txtEgresosTotales.Location = new System.Drawing.Point(190, 125);
            this.txtEgresosTotales.Size = new System.Drawing.Size(180, 20);

            this.lblMontoSolicitado.Text = "Monto Solicitado";
            this.lblMontoSolicitado.Location = new System.Drawing.Point(20, 160);
            this.lblMontoSolicitado.Size = new System.Drawing.Size(150, 20);
            this.txtMontoSolicitado.Location = new System.Drawing.Point(190, 160);
            this.txtMontoSolicitado.Size = new System.Drawing.Size(180, 20);

            this.lblPlazoSolicitado.Text = "Plazo Solicitado (meses)";
            this.lblPlazoSolicitado.Location = new System.Drawing.Point(20, 195);
            this.lblPlazoSolicitado.Size = new System.Drawing.Size(150, 20);
            this.txtPlazoSolicitado.Location = new System.Drawing.Point(190, 195);
            this.txtPlazoSolicitado.Size = new System.Drawing.Size(180, 20);

            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Location = new System.Drawing.Point(190, 235);
            this.btnConsultar.Size = new System.Drawing.Size(100, 30);
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);

            this.ClientSize = new System.Drawing.Size(420, 300);
            this.Text = "Consultar Preaprobacion Credito";
            this.Controls.Add(this.lblTipoDoc);
            this.Controls.Add(this.txtTipoDoc);
            this.Controls.Add(this.lblNroDoc);
            this.Controls.Add(this.txtNroDoc);
            this.Controls.Add(this.lblIngresosTotales);
            this.Controls.Add(this.txtIngresosTotales);
            this.Controls.Add(this.lblEgresosTotales);
            this.Controls.Add(this.txtEgresosTotales);
            this.Controls.Add(this.lblMontoSolicitado);
            this.Controls.Add(this.txtMontoSolicitado);
            this.Controls.Add(this.lblPlazoSolicitado);
            this.Controls.Add(this.txtPlazoSolicitado);
            this.Controls.Add(this.btnConsultar);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}