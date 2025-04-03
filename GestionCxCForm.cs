using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class GestionCxCForm : Form
    {
        public GestionCxCForm()
        {
            InitializeComponent();
            CargarCuentas();
        }

        private void CargarCuentas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    string query = @"SELECT c.ID_CxC, f.ID_Factura, cl.Nombre + ' ' + cl.Apellido AS Cliente,
                                   f.Fecha, f.Total, c.Balance, c.Fecha_Vencimiento
                                   FROM CxC c
                                   INNER JOIN Cabecera_Factura f ON c.ID_Factura = f.ID_Factura
                                   INNER JOIN Clientes cl ON f.ID_Cliente = cl.ID_Cliente
                                   WHERE c.Estado = 'A'";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvCxC.DataSource = dt;
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cuentas: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvCxC.Columns["ID_CxC"].Visible = false;
            dgvCxC.Columns["ID_Factura"].HeaderText = "Factura";
            dgvCxC.Columns["Cliente"].HeaderText = "Cliente";
            dgvCxC.Columns["Fecha"].HeaderText = "Fecha Factura";
            dgvCxC.Columns["Total"].HeaderText = "Total";
            dgvCxC.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["Balance"].HeaderText = "Saldo";
            dgvCxC.Columns["Balance"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["Fecha_Vencimiento"].HeaderText = "Vencimiento";
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (dgvCxC.SelectedRows.Count == 0) return;

            int idCxC = (int)dgvCxC.SelectedRows[0].Cells["ID_CxC"].Value;
            decimal balance = (decimal)dgvCxC.SelectedRows[0].Cells["Balance"].Value;

            using (RegistrarPagoForm frm = new RegistrarPagoForm(idCxC, balance))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarCuentas();
                }
            }
        }
    }
}