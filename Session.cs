namespace PuntoVenta
{
    public static class Session
    {
        public static string NombreUsuario { get; set; }
        public static string Rol { get; set; }
        public static string ConnectionString { get; } = "Server=PF1STQJG;Database=PuntoVentaDB;Trusted_Connection=True;";

        public static void Clear()
        {
            NombreUsuario = null;
            Rol = null;
        }
    }
}