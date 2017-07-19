namespace TeamBuilder.Data
{
    using Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
            : base("name=TeamBuilderContext")
        {
            
        }
        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(c => c.Teams)
                .WithMany(e => e.Events)
                .Map(m =>
                {
                    m.MapLeftKey("EventId");
                    m.MapRightKey("TeamId");
                    m.ToTable("EventTeams");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Teams)
                .WithMany(t => t.Members)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TeamId");
                    m.ToTable("UserTeams");
                });


            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedTeams)
                .WithRequired(t => t.Creator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedEvents)
                .WithRequired(e => e.Creator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedInvitations)
                .WithRequired(i => i.InvitedUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().Property(u=> u.Username)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName
                    , new IndexAnnotation(new IndexAttribute("IX_User_Username", 1) { IsUnique = true }));

            modelBuilder.Entity<Team>().Property(t=> t.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName
                    , new IndexAnnotation(new IndexAttribute("IX_Team_Name", 1) { IsUnique = true }));

            modelBuilder.Entity<Event>().Property(e => e.Name).IsUnicode(true);
            modelBuilder.Entity<Event>().Property(e => e.Description).IsUnicode(true);

            modelBuilder.Entity<Invitation>()
                .HasRequired(i => i.Team)
                .WithMany(t => t.Invitations)
                .WillCascadeOnDelete(false);
        }
    }
}