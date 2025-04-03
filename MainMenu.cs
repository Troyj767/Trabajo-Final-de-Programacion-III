using System;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            ConfigurarMenuSegunRol();
        }

        private void ConfigurarMenuSegunRol()
        {
            lblUsuario.Text = $"Usuario: {Session.NombreUsuario}";
            lblRol.Text = $"Rol: {Session.Rol}";

            // Configurar visibilidad según rol
            bool isAdmin = Session.Rol == "Admin";

            // Menú Registros
            clientesToolStripMenuItem.Visible = true;
            productosToolStripMenuItem.Visible = true;
            usuariosToolStripMenuItem.Visible = isAdmin;

            // Menú Ventas
            nuevaFacturaToolStripMenuItem.Visible = true;
            consultarFacturasToolStripMenuItem.Visible = true;

            // Menú Cuentas
            gestionCxCToolStripMenuItem.Visible = true;

            // Menú Reportes
            stockToolStripMenuItem.Visible = true;
            ventasDiariasToolStripMenuItem.Visible = true;

            // Menú Utilitarios
            cerrarSesiónToolStripMenuItem.Visible = true;
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ClientesForm();
            frm.ShowDialog();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ProductosForm();
            frm.ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Session.Rol == "Admin")
            {
                var frm = new Forms.UsuariosForm();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Acceso restringido a administradores", "Permisos insuficientes",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nuevaFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new Forms.NuevaFacturaForm();
            frm.ShowDialog();
        }

        private void consultarFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ConsultaFacturasForm();
            frm.ShowDialog();
        }

        private void gestionCxCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionCxCForm();
            frm.ShowDialog();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ReporteStockForm();
            frm.ShowDialog();
        }

        private void ventasDiariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ReporteVentasForm();
            frm.ShowDialog();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.Clear();
            this.Hide();
            var login = new LoginForm();
            login.ShowDialog();
            this.Close();
        }
    }
}