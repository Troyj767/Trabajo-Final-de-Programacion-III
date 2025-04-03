using System;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ClienteEditForm : Form
    {
        public dynamic Cliente { get; private set; }

        public ClienteEditForm(dynamic cliente = null)
        {
            InitializeComponent();
            Cliente = cliente ?? new
            {
                ID = 0,
                Nombre = "",
                Apellido = "",
                Telefono = "",
                Correo = "",
                Direccion = ""
            };
            LoadData();
        }

        private void LoadData()
        {
            if (Cliente.ID > 0)
            {
                txtNombre.Text = Cliente.Nombre;
                txtApellido.Text = Cliente.Apellido;
                txtTelefono.Text = Cliente.Telefono;
                txtCorreo.Text = Cliente.Correo;
                txtDireccion.Text = Cliente.Direccion;
                Text = "Editar Cliente";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            Cliente.Nombre = txtNombre.Text;
            Cliente.Apellido = txtApellido.Text;
            Cliente.Telefono = txtTelefono.Text;
            Cliente.Correo = txtCorreo.Text;
            Cliente.Direccion = txtDireccion.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}