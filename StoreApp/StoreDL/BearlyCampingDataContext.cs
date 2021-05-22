using Microsoft.EntityFrameworkCore;
using StoreModels;


namespace StoreDL

{
    public class BearlyCampingDataContext : DbContext
    {
        public BearlyCampingDataContext() : base()
        {

        }
        // Constructer needed to pass in connection string
        public BearlyCampingDataContext(DbContextOptions options) : base(options)
        {

        }
        
        // Declaring Entities that we want to work with
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
            .Property(store => store.StoreID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
            .Property(order => order.OrderNumber)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Inventory>()
                .HasKey(inventory => inventory.ISBN);
            modelBuilder.Entity<Order>()
                .HasKey(order => order.OrderNumber);
            modelBuilder.Entity<Product>()
                .HasKey(product => product.ISBN);
            modelBuilder.Entity<Transaction>()
                .HasKey(transact => transact.OrderNumber);
            modelBuilder.Entity<User>()
                .HasKey(user => user.UserName);
        }


    }
}