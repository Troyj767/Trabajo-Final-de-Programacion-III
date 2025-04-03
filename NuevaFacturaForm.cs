using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta.Forms
{
    public partial class NuevaFacturaForm : Form
    {
        private string connectionString = "Server=PF1STQJG;Database=PuntoVentaDB;Trusted_Connection=True;"; // Replace with your connection string

        public NuevaFacturaForm()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID_Producto, Nombre, Precio FROM Productos"; // Ensure you select ID_Producto
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Columns.Contains("ID_Producto"))
                        {
                            cmbProductos.DataSource = dt;
                            cmbProductos.DisplayMember = "Nombre";
                            cmbProductos.ValueMember = "ID_Producto";
                        }
                        else
                        {
                            MessageBox.Show("La columna 'ID_Producto' no existe en el DataTable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron productos en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem != null && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                DataRowView selectedProduct = (DataRowView)cmbProductos.SelectedItem;
                string nombre = selectedProduct["Nombre"].ToString();
                decimal precio = Convert.ToDecimal(selectedProduct["Precio"]);
                decimal total = precio * cantidad;

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    if (row.Cells["Nombre"].Value.ToString() == nombre)
                    {
                        int existingCantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        row.Cells["Cantidad"].Value = existingCantidad + cantidad; // Update quantity
                        row.Cells["Total"].Value = (existingCantidad + cantidad) * precio; // Update total
                        CalcularTotal();
                        return; // Exit method as it has been updated
                    }
                }

                dgvDetalle.Rows.Add(nombre, precio, cantidad, total); // Add new product
                CalcularTotal();
            }
            else
            {
                MessageBox.Show("Seleccione un producto y asegúrese de ingresar una cantidad válida mayor que cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la factura para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        string query = "INSERT INTO Facturas (Fecha, Total) VALUES (@Fecha, @Total); SELECT SCOPE_IDENTITY();"; // Insert and get ID
                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Total", lblTotal.Text.Replace("Total: ", "").Replace("$", ""));
                        int facturaId = Convert.ToInt32(cmd.ExecuteScalar()); // Get the ID of the inserted invoice

                        // Insert details
                        foreach (DataGridViewRow row in dgvDetalle.Rows)
                        {
                            string detalleQuery = "INSERT INTO DetallesFactura (FacturaID, ProductoID, Cantidad, Precio) VALUES (@FacturaID, @ProductoID, @Cantidad, @Precio)";
                            SqlCommand detalleCmd = new SqlCommand(detalleQuery, conn, transaction);
                            detalleCmd.Parameters.AddWithValue("@FacturaID", facturaId);
                            detalleCmd.Parameters.AddWithValue("@ProductoID", GetProductoID(row.Cells["Nombre"].Value.ToString())); // Get product ID
                            detalleCmd.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(row.Cells["Cantidad"].Value));
                            detalleCmd.Parameters.AddWithValue("@Precio", Convert.ToDecimal(row.Cells["Precio"].Value));
                            detalleCmd.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Confirm transaction
                    }
                    catch
                    {
                        transaction.Rollback(); // Roll back on error
                        throw;
                    }
                }

                MessageBox.Show("Factura guardada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario(); // Clear form data
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetProductoID(string nombre)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID_Producto FROM Productos WHERE Nombre = @Nombre";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void LimpiarFormulario()
        {
            dgvDetalle.Rows.Clear();
            lblTotal.Text = "Total: $0.00";
            txtCantidad.Clear();
            cmbProductos.SelectedIndex = -1; // Reset combo box
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Total"].Value);
            }
            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }
    }
}