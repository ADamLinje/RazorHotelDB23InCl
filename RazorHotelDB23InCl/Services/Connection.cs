namespace RazorHotelDB23InCl.Services
{
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Secret.connectionString;
            //connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            //connectionString = Configuration["ConnectionStrings:SimplyConnection"];
        }

        public Connection(string connection)
        {
            connectionString = connection;
        }

    }

}
