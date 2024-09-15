using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Database.API.Models
{
    public class InventoryDbContext : IdentityDbContext<User, Role, Guid>
    {
        public InventoryDbContext()
        {

        }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLocation> ItemLocations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().ToTable("Items");
            modelBuilder.Entity<ItemLocation>().ToTable("ItemLocations");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Item>().HasOne(i => i.ItemLocation).WithMany(il => il.Items).HasForeignKey(i => i.ItemLocationId);
            modelBuilder.Entity<Item>().HasOne(i => i.CreateUser).WithMany(u => u.Items).HasForeignKey(i => i.CreateUserId);
            modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            // Seed data for Item
            var location1Id = Guid.NewGuid();
            var location2Id = Guid.NewGuid();

            // Generate UserGuid values
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            // Seed data for ItemLocation
            modelBuilder.Entity<ItemLocation>().HasData(
                new ItemLocation
                {

                    LocationId = location1Id,
                    LocationName = "Warehouse A",
                    LocationDetail = "Main Warehouse"
                },
                new ItemLocation
                {

                    LocationId = location2Id,
                    LocationName = "Warehouse B",
                    LocationDetail = "Secondary Warehouse"
                }
            );
            //Seed data for role
            modelBuilder.Entity<Role>().HasData(
                new Role { Id=Guid.Parse("4D206423-BD5C-4819-A8F4-3B6BB5C26E28"),Name="Adminstrator",NormalizedName="ADMINSTRATOR" },
                new Role { Id= Guid.Parse("1573D5E5-F41F-4F25-926B-2009D7DDD302"),Name="User",NormalizedName="USER" }
                );
            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                    new User
     {
         Id = user1Id,  // Use a GUID value
         UserName = "Admin",
         NormalizedUserName = "ADMIN",
         Email = "admin@example.com",
         NormalizedEmail = "ADMIN@EXAMPLE.COM",
         RoleLevel = 1,
         PersonName="芳儀",
         Department = "Management",
         PasswordHash = new PasswordHasher<User>().HashPassword(null, "Admin@123") // Example password
     },
                    new User
     {
         Id = user2Id,  // Use a GUID value
         UserName = "John Doe",
         NormalizedUserName = "JOHN DOE",
         Email = "john.doe@example.com",
         NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
         RoleLevel = 2,
         PersonName="白菜",
         Department = "Sales",
         PasswordHash = new PasswordHasher<User>().HashPassword(null, "John@123") // Example password
     }
                );

            // Seed data for Item
            modelBuilder.Entity<Item>().HasData(
                new Item
                {

                    ItemId = Guid.NewGuid(),
                    ItemName = "Item 1",
                    PurchaseId = 1001,
                    ItemLocationId = location1Id,
                    LocationName = "Warehouse A",
                    Description = "Item 1 Description",
                    CreateUserId = user1Id,
                    Status = (ItemStatus.正常).ToString()
                },
                new Item
                {

                    ItemId = Guid.NewGuid(),
                    ItemName = "Item 2",
                    PurchaseId = 1002,
                    ItemLocationId = location2Id,
                    LocationName = "Warehouse B",
                    Description = "Item 2 Description",
                    CreateUserId = user2Id,
                    Status = (ItemStatus.正常).ToString()
                });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId= "4D206423-BD5C-4819-A8F4-3B6BB5C26E28",UserId=user1Id.ToString()},
                new IdentityUserRole<string> { RoleId= "1573D5E5-F41F-4F25-926B-2009D7DDD302",UserId=user2Id.ToString() }
                
                );
        }
    }
}
