namespace RazorHotelDB23InCl.Services
{
    public class Secret
    {
        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string connectionString
        {
            get { return _connectionString; }
        }

    }
}
