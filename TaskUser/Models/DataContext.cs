using Microsoft.EntityFrameworkCore;
using TaskUser.Encryption;
using TaskUser.Models.Production;
using TaskUser.Models.Sales;

namespace TaskUser.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)  
        {  
        }

        public DbSet<User> Users { get; set;}
        public DbSet<Store> Stores  { get; set;}
        public DbSet<Brand> Brands  { get; set;}
        public DbSet<Category> Categories  { get; set;}
        public DbSet<Product> Products  { get; set;}
        public DbSet<Stock> Stocks  { get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(t => t.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(t => t.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(t => t.PassWord).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(t => t.Phone).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(t => t.Role);
            modelBuilder.Entity<User>().Property(t => t.IsActiver);
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<Store>().Property(t => t.StoreName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Store>().Property(t => t.Email).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Store>().Property(t => t.Phone).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Store>().Property(t => t.Street).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Store>().Property(t => t.State).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Store>().Property(t => t.City).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Store>().Property(t => t.ZipCode).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Customer>().Property(t => t.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(t => t.Email).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(t => t.Phone).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Customer>().Property(t => t.Street).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(t => t.State).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Customer>().Property(t => t.City).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(t => t.ZipCode).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Brand>().Property(t => t.BrandName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Category>().Property(t => t.CategoryName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().Property(t => t.ProductName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Product>().Property(t => t.BrandId).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.CategoryId).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.BrandId).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.ModelYear).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.Picture).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Product>().Property(t => t.ListPrice).IsRequired();
            modelBuilder.Entity<Stock>().ToTable("Stock");
            modelBuilder.Entity<Stock>().Property(t => t.StoreId).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Stock>().Property(t => t.ProductId).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Stock>().Property(t => t.Quantity).IsRequired();
            
            // ForeignKey
            modelBuilder.Entity<User>().HasOne(s => s.Store).WithMany(g => g.Users).HasForeignKey(s => s.StoreId);
            
            modelBuilder.Entity<Product>().HasOne(s => s.Categorie).WithMany(g => g.Products).HasForeignKey(s => s.CategoryId);
            modelBuilder.Entity<Product>().HasOne(s => s.Brand).WithMany(g => g.Product).HasForeignKey(s => s.BrandId);
            
            modelBuilder.Entity<Stock>().HasOne(s => s.Store).WithMany(g => g.Stocks).HasForeignKey(s => s.StoreId);
            modelBuilder.Entity<Stock>().HasOne(s => s.Product).WithMany(g => g.Stocks).HasForeignKey(s => s.ProductId);
   
            
            

            
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Email = "Vanloc@gmail.com",
                    PassWord = SecurePasswordHasher.Hash("10101997"),
                    Name = "Loc",
                    StoreId=1,
                    Phone = "123456789",
                    Role = 1,
                    IsActiver = true
                },
                new User()
                {
                    Id = 2,
                    Email = "Vanloc1@gmail.com",
                    PassWord = SecurePasswordHasher.Hash("10101997"),
                    Name = "Loc",
                    StoreId=2,
                    Phone = "123456789",
                    Role = 2,
                    IsActiver = true
                }
            );
            modelBuilder.Entity<Store>().HasData(
                new Store()
                {
                    Id = 1,
                    StoreName = "Tui sach",
                    Email = "Vanloc@gmail.com",
                    City = "hue",
                    Phone = "123456789",
                    Street = "Ha Noi",
                    State = "khong",
                    ZipCode = "51634"
                }
            );
            
            modelBuilder.Entity<Brand>().HasData(
                new Brand()
                {
                    Id = 1,
                    BrandName = "ao da"
                }
            );
            
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    CategoryName = "Ao"
                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    BrandId = 1,
                    CategoryId = 1,
                    ModelYear = 1,
                    ProductName = "Ao da so 1",
                    Picture = "",
                    ListPrice = 123456
                }
            );
            modelBuilder.Entity<Stock>().HasKey(st => new {st.ProductId, st.StoreId});
            modelBuilder.Entity<Stock>().HasData(
                new Stock()
                {
                    StoreId = 1,
                    ProductId = 1,
                    Quantity = 1
                }
            );

            
          
      
        }
        
    }
}