using StoreModels;
using StoreBL;
using StoreDL;  
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace StoreUI
{
    /// <summary>
    /// Class to menufacture menus using factory dp
    /// </summary>

    public class MenuFactory
    {
        public static StoreMenu GetMenu(string menuType, User CurrentUser)
        {
            // getting configurations from a config file
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // setting up my db connections
            string connectionString = configuration.GetConnectionString("BearlyCampingDB");
            // we're building the dbcontext using the constructor that takes in options, we're setting the connection
            // string outside the context def'n
            DbContextOptions<StoreDL.BearlyCampingDataContext> options = new DbContextOptionsBuilder<StoreDL.BearlyCampingDataContext>()
            .UseNpgsql(connectionString)
            .Options;
            // passing the options we just built
            var context = new StoreDL.BearlyCampingDataContext(options);

            // Initialize Serilogger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("../logs/StoreApp.txt", rollingInterval : RollingInterval.Day)
            .CreateLogger();

            StoreBLInterface BussinessLayer = new StoreBussinessLayer(new RepoDB(context));

            switch (menuType.ToLower())
            {
                case "login":
                    return new LoginMenu(BussinessLayer);
                case "editproduct":
                    return new EditProductMenu(BussinessLayer);
                case "home":
                    return new HomeMenu(CurrentUser);
                case "order":
                    return new OrderMenu(BussinessLayer, CurrentUser);
                case "manager":
                    return new ManagerMenu(BussinessLayer, CurrentUser);
                case "customerorders":
                    return new CustomerOrdersMenu(BussinessLayer, CurrentUser);
                default:
                    return null;
            }
        }
    }
}