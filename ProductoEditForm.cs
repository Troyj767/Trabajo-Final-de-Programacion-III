using System;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class ProductoEditForm : Form
    {
        public Producto Producto { get; private set; }

        public ProductoEditForm(Producto producto = null)
        {
            InitializeComponent();
            Producto = producto ?? new Producto();
            LoadData();
        }

        private void LoadData()
        {
            if (Producto.ID_Producto > 0)
            {
                txtCodigo.Text = Producto.Codigo;
                txtNombre.Text = Producto.Nombre;
                txtDescripcion.Text = Producto.Descripcion;
                nudPrecio.Value = Producto.Precio;
                nudStock.Value = Producto.Stock;
                Text = "Editar Producto";
            }
            else
            {
                Text = "Nuevo Producto";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            Producto.Codigo = txtCodigo.Text;
            Producto.Nombre = txtNombre.Text;
            Producto.Descripcion = txtDescripcion.Text;
            Producto.Precio = nudPrecio.Value;
            Producto.Stock = (int)nudStock.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El código es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}