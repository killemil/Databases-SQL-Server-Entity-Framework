namespace Excercise05
{
    using System.Data.Entity;
    using Models;
    using Migrations;

    public class PhotoDbContext : DbContext
    {
        public PhotoDbContext()
            : base("name=PhotoDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoDbContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<PhotoDbContext>());
        }
        
        public virtual DbSet<Photographer> Photographers { get; set; }
        public virtual DbSet<Picture>  Pictures { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<PhotographerAlbum> PhotographerAlbums { get; set; }

    }
}