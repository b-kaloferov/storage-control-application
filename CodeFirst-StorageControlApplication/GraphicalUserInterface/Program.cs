using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace GraphicalUserInterface
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var optionsBuilder = new DbContextOptionsBuilder<StorageDbContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-UNHGGLSQ\\SQLEXPRESS;Database=StorageDb;Trusted_Connection=True;TrustServerCertificate=True;");
            using (var storageDbContext = new StorageDbContext(optionsBuilder.Options))
            {
                var modelsContext = new ModelsContext(storageDbContext);
                var modelService = new ModelService(modelsContext);

                var clientsContext = new ClientsContext(storageDbContext);
                var clientService = new ClientService(clientsContext);

                var shoesContext = new ShoesContext(storageDbContext);
                var shoeService = new ShoeService(shoesContext);

                var orderDetailsContext = new OrderDetailsContext(storageDbContext);


                var ordersContext = new OrdersContext(storageDbContext);
                var orderService = new OrderService(ordersContext, orderDetailsContext, clientsContext, shoesContext);
                Application.Run(new Form1(modelService, clientService, shoeService));
            }  
        }
    }
}