namespace PhotographyWorkshop.Data
{
    using Models;
    using System.Data.Entity;

    public class PhotographyContext : DbContext
    {
        public PhotographyContext()
            : base("name=PhotographyContext")
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; }

        public virtual DbSet<Camera> Cameras { get; set; }

        public virtual DbSet<Lens> Lenses { get; set; }

        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<Workshop> Workshops { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photographer>()
                .HasRequired(p => p.PrimaryCamera)
                .WithMany(c => c.PrimaryCamerasPhotographers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasRequired(p => p.SecondaryCamera)
                .WithMany(c => c.SecondaryCamerssPhotographes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasMany(p => p.WorkshopsParticipate)
                .WithMany(w => w.Participants)
                .Map(entity =>
                {
                    entity.ToTable("WorkshopParticipants");
                    entity.MapLeftKey("WorkshopId");
                    entity.MapRightKey("PhotographerId");
                });

            modelBuilder.Entity<Workshop>()
                .HasRequired(w => w.Trainer)
                .WithMany(p => p.WorkshopTrainer)
                .WillCascadeOnDelete(false);

        }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}