using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MusicShopBackend.Entities
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<DestinationAddress> DestinationAddresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MusicShopDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(o => new { o.OrderId, o.ProductId });

            modelBuilder.Entity<Brand>()
                .HasData(new
                {
                    BrandId = 1,
                    BrandName = "Versace"
                });

            modelBuilder.Entity<Category>()
                .HasData(new
                {
                    CategoryId = 1,
                    CategoryName = "SeedTest",
                    CategoryDescription = "SeedDesc"
                });

            modelBuilder.Entity<CreditCard>()
                .HasData(new
                {
                    CreditCardId = 1,
                    CreditCardNumber = "124128401",
                    Cvv = 123,
                    ExpireDate = DateTime.Now
                });


            modelBuilder.Entity<DestinationAddress>()
                .HasData(new
                {
                    DestinationAddressId = 1,
                    City = "New York",
                    Country = "USA",
                    ZipCode = "14000",
                    PhoneNumber = "12847124",
                    Address = "Boulevard of broken dreams 2"
                });
            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = 1,
                    RoleName = "User"
                });

            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = 2,
                    RoleName = "Employee"
                });

            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = 3,
                    RoleName = "Admin"
                });

            modelBuilder.Entity<Employee>()
                .HasData(new
                {
                    EmployeeId = 1,
                    FirstName = "Luka",
                    LastName = "Panic",
                    Email = "lukap181@gmail.com",
                    Password = "123252",
                    Contact = "0638338953",
                    City = "Novi Sad",
                    RoleId = 2
                });


            modelBuilder.Entity<User>()
               .HasData(new
               {
                   UserId = 1,
                   FirstName = "Marko",
                   LastName = "Milic",
                   Email = "Marko@gmail.com",
                   Password = "123153",
                   RoleId = 1
               });


            modelBuilder.Entity<Order>()
                .HasData(new
                {
                    OrderId = 1,
                    OrderDate = DateTime.Now,
                    OrderArrival = true,
                    PaymentType = "seedTest",
                    OrderStatus = "Ready",
                    UserId = 1,
                    CreditCardId = 1,
                    DestinationAddressId = 1
                });

            modelBuilder.Entity<Product>()
                 .HasData(new
                 {
                     ProductId = 1,
                     ProductName = "Guitar",
                     ProductPrice = 23.00,
                     ProductDescription = "ACoustic",
                     CategoryId = 1,
                     EmployeeId = 1,
                     BrandId = 1
                 });

            modelBuilder.Entity<OrderProduct>()
                .HasData(new
                {
                    OrderId = 1,
                    ProductId = 1,
                    OrderQuantity = 34
                });


        }
    }
}
