using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class RegistrarPagoForm : Form
    {
        private readonly int _idCxC;

        public RegistrarPagoForm(int idCxC, decimal balance)
        {
            InitializeComponent();
            _idCxC = idCxC;
            lblBalance.Text = balance.ToString("C2");
            nudMonto.Maximum = (decimal)balance;
            dtpFecha.Value = DateTime.Today;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (nudMonto.Value <= 0)
            {
                MessageBox.Show("Ingrese un monto válido", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    conn.Open();

                    // Registrar pago
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO PagosCxC (ID_CxC, Fecha, Monto, FormaPago) " +
                        "VALUES (@ID_CxC, @Fecha, @Monto, @FormaPago)", conn);

                    cmd.Parameters.AddWithValue("@ID_CxC", _idCxC);
                    cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value);
                    cmd.Parameters.AddWithValue("@Monto", nudMonto.Value);
                    cmd.Parameters.AddWithValue("@FormaPago", cboFormaPago.Text);
                    cmd.ExecuteNonQuery();

                    // Actualizar saldo
                    cmd = new SqlCommand(
                        "UPDATE CxC SET Balance = Balance - @Monto " +
                        "WHERE ID_CxC = @ID_CxC", conn);

                    cmd.Parameters.AddWithValue("@ID_CxC", _idCxC);
                    cmd.Parameters.AddWithValue("@Monto", nudMonto.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Pago registrado exitosamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar pago: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}