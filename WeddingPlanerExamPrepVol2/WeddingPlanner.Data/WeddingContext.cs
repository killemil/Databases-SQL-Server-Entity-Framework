namespace WeddingPlanner.Data
{
    using Models;
    using System.Data.Entity;

    public class WeddingContext : DbContext
    {
        public WeddingContext()
            : base("name=WeddingContext")
        {
        }

        public virtual DbSet<Agency> Agencies { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Present> Presents { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        public virtual DbSet<Wedding> Weddings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Brides)
                .WithRequired(w => w.Bride)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Bridegrooms)
                .WithRequired(w => w.Bridegroom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Invitations)
                .WithRequired(i => i.Guest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wedding>()
                .HasMany(w => w.Invitations)
                .WithRequired(i => i.Wedding)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wedding>()
                .HasMany(w => w.Venues)
                .WithMany(v => v.Weddings)
                .Map(map =>
                {
                    map.ToTable("WeddingVenues");
                    map.MapLeftKey("WeddingId");
                    map.MapRightKey("VenueId");
                });

            modelBuilder.Entity<Present>()
                .HasKey(p => p.InvitationId);

            modelBuilder.Entity<Invitation>()
                .HasRequired(i => i.Present)
                .WithRequiredPrincipal(p => p.Invitation)
                .WillCascadeOnDelete(false);
        }
    }


}