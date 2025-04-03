namespace PuntoVenta
{
    partial class ReporteStockForm
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

        private void InitializeComponent()
        {
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.lblUmbral = new System.Windows.Forms.Label();
            this.txtUmbral = new System.Windows.Forms.TextBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();

            // dgvReporte
            this.dgvReporte.AllowUserToAddRows = false;
            this.dgvReporte.AllowUserToDeleteRows = false;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(12, 50);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ReadOnly = true;
            this.dgvReporte.Size = new System.Drawing.Size(760, 350);

            // lblUmbral
            this.lblUmbral.AutoSize = true;
            this.lblUmbral.Location = new System.Drawing.Point(12, 20);
            this.lblUmbral.Text = "Umbral de Stock:";

            // txtUmbral
            this.txtUmbral.Location = new System.Drawing.Point(120, 17);
            this.txtUmbral.Size = new System.Drawing.Size(50, 20);
            this.txtUmbral.Text = "10";

            // btnGenerar
            this.btnGenerar.Location = new System.Drawing.Point(180, 15);
            this.btnGenerar.Text = "Generar Reporte";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);

            // ReporteStockForm
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.dgvReporte);
            this.Controls.Add(this.lblUmbral);
            this.Controls.Add(this.txtUmbral);
            this.Controls.Add(this.btnGenerar);
            this.Text = "Reporte de Stock";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Label lblUmbral;
        private System.Windows.Forms.TextBox txtUmbral;
        private System.Windows.Forms.Button btnGenerar;
    }
}