namespace XML.Data
{
    using Models;
    using System.Data.Entity;

    public class XmlContext : DbContext
    {
        public XmlContext()
            : base("name=XmlContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(
                    uf =>
                    {
                        uf.MapLeftKey("UserId");
                        uf.MapRightKey("FriendId");
                        uf.ToTable("UserFriends");
                    });

            modelBuilder.Entity<User>().HasMany(u => u.SoldProducts).WithRequired(up => up.Seller);
            modelBuilder.Entity<User>().HasMany(u => u.BoughtProducts).WithOptional(p => p.Buyer);

            base.OnModelCreating(modelBuilder);
        }
    }


}