namespace SolarSystem.Data
{
    using Models;
    using System.Data.Entity;

    public class SolarSystemContext : DbContext
    {
        public SolarSystemContext()
            : base("name=SolarSystemContext")
        {
        }

        public virtual DbSet<Anomaly> Anomalies { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasMany(p => p.OriginAnomalies)
                .WithRequired(a => a.OriginPlanet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Planet>()
                .HasMany(p => p.TeleportAnomalies)
                .WithRequired(a => a.TeleportPlanet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasMany(a => a.Victims)
                .WithMany(p => p.Anomalies)
                .Map(map =>
                {
                    map.ToTable("AnomalyVictims");
                    map.MapLeftKey("AnomalyId");
                    map.MapRightKey("PersonId");
                });

            modelBuilder.Entity<SolarSystem>()
                .HasMany(s => s.Stars)
                .WithRequired(s => s.SolarSystem)
                .WillCascadeOnDelete(false);
        }
    }

}