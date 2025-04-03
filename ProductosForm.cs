using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ProductosForm : Form
    {
        public ProductosForm()
        {
            InitializeComponent();
            LoadProductos();
        }

        private void LoadProductos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvProductos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var frm = new ProductoEditForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadProductos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;

            var row = ((DataRowView)dgvProductos.SelectedRows[0].DataBoundItem).Row;
            var producto = new Producto
            {
                ID_Producto = (int)row["ID_Producto"],
                Codigo = row["Codigo"].ToString(),
                Nombre = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"].ToString(),
                Precio = (decimal)row["Precio"],
                Stock = (int)row["Stock"]
            };

            using (var frm = new ProductoEditForm(producto))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadProductos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;

            int id = (int)((DataRowView)dgvProductos.SelectedRows[0].DataBoundItem).Row["ID_Producto"];

            if (MessageBox.Show("¿Eliminar este producto?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Productos WHERE ID_Producto = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                    LoadProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}