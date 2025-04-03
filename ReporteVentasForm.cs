using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ReporteVentasForm : Form
    {
        public ReporteVentasForm()
        {
            InitializeComponent();
            dtpFechaInicio.Value = DateTime.Today.AddDays(-7);
            dtpFechaFin.Value = DateTime.Today;
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ReporteVentasDiarias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", dtpFechaInicio.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", dtpFechaFin.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        dgvVentas.DataSource = ds.Tables[0];
                        ConfigurarColumnas();

                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            DataRow resumen = ds.Tables[1].Rows[0];
                            lblTotal.Text = $"Total Ventas: {Convert.ToDecimal(resumen["TotalVentas"]):C2}";
                            lblContado.Text = $"Contado: {Convert.ToDecimal(resumen["VentasContado"]):C2}";
                            lblCredito.Text = $"Crédito: {Convert.ToDecimal(resumen["VentasCredito"]):C2}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvVentas.Columns["ID_Factura"].HeaderText = "Factura";
            dgvVentas.Columns["Cliente"].HeaderText = "Cliente";
            dgvVentas.Columns["Fecha"].HeaderText = "Fecha";
            dgvVentas.Columns["Total"].HeaderText = "Total";
            dgvVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvVentas.Columns["Tipo_Pago"].HeaderText = "Tipo Pago";
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}