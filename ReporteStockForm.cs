using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ReporteStockForm : Form
    {
        public ReporteStockForm()
        {
            InitializeComponent();
            CargarReporte(10); // Umbral por defecto
        }

        private void CargarReporte(int umbral)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarProductosBajoStock", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Umbral", umbral);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvReporte.DataSource = dt;
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reporte: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvReporte.Columns["Codigo"].HeaderText = "Código";
            dgvReporte.Columns["Nombre"].HeaderText = "Producto";
            dgvReporte.Columns["Stock"].HeaderText = "Stock Actual";
            dgvReporte.Columns["Estado"].HeaderText = "Estado";
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            int umbral;
            if (int.TryParse(txtUmbral.Text, out umbral))
            {
                CargarReporte(umbral);
            }
            else
            {
                MessageBox.Show("Ingrese un umbral válido", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}