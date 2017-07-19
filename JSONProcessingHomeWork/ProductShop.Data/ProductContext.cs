namespace ProductShop.Data
{
    using Models;
    using System.Data.Entity;

    public class ProductContext : DbContext
    {

        public ProductContext()
            : base("name=ProductContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.SoldProducts).WithRequired(up => up.Seller);
            modelBuilder.Entity<User>().HasMany(u => u.BoughtProducts).WithOptional(p => p.Buyer);
            modelBuilder.Entity<User>().HasMany(u => u.Friends).WithMany().Map(mc =>
            {
                mc.MapLeftKey("UserId");
                mc.MapRightKey("FriendId");
                mc.ToTable("UsersFriends");

            });

            base.OnModelCreating(modelBuilder);
        }
    }

}