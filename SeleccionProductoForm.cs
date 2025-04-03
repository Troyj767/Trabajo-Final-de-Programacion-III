using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class SeleccionProductoForm : Form
    {
        public Producto ProductoSeleccionado { get; private set; }

        public SeleccionProductoForm()
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
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT ID_Producto, Codigo, Nombre, Precio FROM Productos WHERE Stock > 0", conn);
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataRowView row = (DataRowView)dgvProductos.SelectedRows[0].DataBoundItem;
                ProductoSeleccionado = new Producto
                {
                    ID_Producto = Convert.ToInt32(row["ID_Producto"]),
                    Codigo = row["Codigo"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Precio = Convert.ToDecimal(row["Precio"])
                };
                DialogResult = DialogResult.OK;
            }
        }
    }
}