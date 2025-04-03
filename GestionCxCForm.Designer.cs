namespace PuntoVenta
{
    partial class GestionCxCForm
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
            this.dgvCxC = new System.Windows.Forms.DataGridView();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCxC)).BeginInit();
            this.SuspendLayout();

            // dgvCxC
            this.dgvCxC.AllowUserToAddRows = false;
            this.dgvCxC.AllowUserToDeleteRows = false;
            this.dgvCxC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCxC.Location = new System.Drawing.Point(12, 12);
            this.dgvCxC.Name = "dgvCxC";
            this.dgvCxC.ReadOnly = true;
            this.dgvCxC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCxC.Size = new System.Drawing.Size(760, 350);

            // btnRegistrarPago
            this.btnRegistrarPago.Location = new System.Drawing.Point(12, 380);
            this.btnRegistrarPago.Text = "Registrar Pago";
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);

            // btnVerDetalle
            this.btnVerDetalle.Location = new System.Drawing.Point(120, 380);
            this.btnVerDetalle.Text = "Ver Detalle";

            // GestionCxCForm
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.dgvCxC);
            this.Controls.Add(this.btnRegistrarPago);
            this.Controls.Add(this.btnVerDetalle);
            this.Text = "Gestión de Cuentas por Cobrar";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCxC)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvCxC;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.Button btnVerDetalle;
    }
}