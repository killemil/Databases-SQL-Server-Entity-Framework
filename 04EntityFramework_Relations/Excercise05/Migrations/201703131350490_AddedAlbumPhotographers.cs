namespace Excercise05.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedAlbumPhotographers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotographerAlbums", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotographerAlbums", "Role");
        }
    }
}
