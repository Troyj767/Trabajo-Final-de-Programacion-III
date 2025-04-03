using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ClientesForm : Form
    {
        public ClientesForm()
        {
            InitializeComponent();
            LoadClientes();
        }

        private void LoadClientes(string filtro = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                {
                    string query = "SELECT ID_Cliente, Nombre, Apellido, Telefono, Correo, Direccion FROM Clientes";

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        query += " WHERE Nombre LIKE @Filtro OR Apellido LIKE @Filtro OR Telefono LIKE @Filtro";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Filtro", $"%{filtro}%");
                    }

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvClientes.DataSource = dt;
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvClientes.Columns["ID_Cliente"].HeaderText = "ID";
            dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
            dgvClientes.Columns["Apellido"].HeaderText = "Apellido";
            dgvClientes.Columns["Telefono"].HeaderText = "Teléfono";
            dgvClientes.Columns["Correo"].HeaderText = "Correo";
            dgvClientes.Columns["Direccion"].HeaderText = "Dirección";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var frm = new ClienteEditForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadClientes();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0) return;

            var row = ((DataRowView)dgvClientes.SelectedRows[0].DataBoundItem).Row;
            var cliente = new
            {
                ID = (int)row["ID_Cliente"],
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Correo = row["Correo"].ToString(),
                Direccion = row["Direccion"].ToString()
            };

            using (var frm = new ClienteEditForm(cliente))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadClientes();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0) return;

            int id = (int)((DataRowView)dgvClientes.SelectedRows[0].DataBoundItem).Row["ID_Cliente"];

            if (MessageBox.Show("¿Eliminar este cliente?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Session.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Clientes WHERE ID_Cliente = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                    LoadClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LoadClientes(txtBuscar.Text);
        }
    }
}