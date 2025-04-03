using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ConsultaFacturasForm : Form
    {
        public ConsultaFacturasForm()
        {
            InitializeComponent();
            CargarFacturas();
        }

        private void CargarFacturas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    string query = @"SELECT f.ID_Factura, c.Nombre + ' ' + c.Apellido AS Cliente, 
                                   f.Fecha, f.Total, f.Tipo_Pago 
                                   FROM Cabecera_Factura f
                                   INNER JOIN Clientes c ON f.ID_Cliente = c.ID_Cliente
                                   ORDER BY f.Fecha DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvFacturas.DataSource = dt;
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar facturas: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvFacturas.Columns["ID_Factura"].HeaderText = "N° Factura";
            dgvFacturas.Columns["Cliente"].HeaderText = "Cliente";
            dgvFacturas.Columns["Fecha"].HeaderText = "Fecha";
            dgvFacturas.Columns["Total"].HeaderText = "Total";
            dgvFacturas.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvFacturas.Columns["Tipo_Pago"].HeaderText = "Tipo Pago";
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count == 0) return;

            int idFactura = (int)dgvFacturas.SelectedRows[0].Cells["ID_Factura"].Value;

            if (MessageBox.Show($"¿Anular factura #{idFactura}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AnularFactura(idFactura);
            }
        }

        private void AnularFactura(int idFactura)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE CxC SET Estado = 'I' WHERE ID_Factura = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", idFactura);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(
                        @"UPDATE p SET p.Stock = p.Stock + d.Cantidad
                        FROM Productos p
                        INNER JOIN Detalle_Factura d ON p.ID_Producto = d.ID_Producto
                        WHERE d.ID_Factura = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", idFactura);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Factura anulada correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarFacturas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al anular factura: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}