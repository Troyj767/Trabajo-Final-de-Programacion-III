using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace PuntoVenta
{
    public partial class LoginForm : Form
    {
        private const string ConnectionString = "Server=PF1STQJG;Database=PuntoVentaDB;Trusted_Connection=True;";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Ingrese usuario y contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hashedPassword = ComputeSha256Hash(password);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("ValidarUsuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreUsuario", username);
                    cmd.Parameters.AddWithValue("@Contraseña", hashedPassword);

                    SqlParameter isValid = new SqlParameter("@EsValido", System.Data.SqlDbType.Bit);
                    isValid.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(isValid);

                    SqlParameter message = new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar, 200);
                    message.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(message);

                    cmd.ExecuteNonQuery();

                    if ((bool)isValid.Value)
                    {
                        cmd = new SqlCommand("SELECT Nombre, Rol FROM Usuarios WHERE NombreUsuario = @Usuario", conn);
                        cmd.Parameters.AddWithValue("@Usuario", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session.NombreUsuario = reader["Nombre"].ToString();
                                Session.Rol = reader["Rol"].ToString();

                                this.Hide();
                                new MainMenu().Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(message.Value.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}