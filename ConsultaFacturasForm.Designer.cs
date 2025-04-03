namespace PuntoVenta
{
    partial class ConsultaFacturasForm
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
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();

            // dgvFacturas
            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Location = new System.Drawing.Point(12, 40);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(760, 350);

            // btnAnular
            this.btnAnular.Location = new System.Drawing.Point(12, 400);
            this.btnAnular.Text = "Anular Factura";
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);

            // btnVerDetalle
            this.btnVerDetalle.Location = new System.Drawing.Point(120, 400);
            this.btnVerDetalle.Text = "Ver Detalle";

            // txtBuscar
            this.txtBuscar.Location = new System.Drawing.Point(12, 12);
            this.txtBuscar.Size = new System.Drawing.Size(200, 20);

            // btnBuscar
            this.btnBuscar.Location = new System.Drawing.Point(220, 10);
            this.btnBuscar.Text = "Buscar";

            // ConsultaFacturasForm
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.dgvFacturas);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnBuscar);
            this.Text = "Consulta de Facturas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
    }
}