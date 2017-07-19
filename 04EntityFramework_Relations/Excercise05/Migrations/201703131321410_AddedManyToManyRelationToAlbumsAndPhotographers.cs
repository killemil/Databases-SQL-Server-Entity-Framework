namespace Excercise05.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedManyToManyRelationToAlbumsAndPhotographers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "PhotographerId", "dbo.Photographers");
            DropIndex("dbo.Albums", new[] { "PhotographerId" });
            CreateTable(
                "dbo.PhotographerAlbums",
                c => new
                    {
                        Photographer_Id = c.Int(nullable: false),
                        Album_AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photographer_Id, t.Album_AlbumId })
                .ForeignKey("dbo.Photographers", t => t.Photographer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumId, cascadeDelete: true)
                .Index(t => t.Photographer_Id)
                .Index(t => t.Album_AlbumId);
            
            DropColumn("dbo.Albums", "PhotographerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "PhotographerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PhotographerAlbums", "Album_AlbumId", "dbo.Albums");
            DropForeignKey("dbo.PhotographerAlbums", "Photographer_Id", "dbo.Photographers");
            DropIndex("dbo.PhotographerAlbums", new[] { "Album_AlbumId" });
            DropIndex("dbo.PhotographerAlbums", new[] { "Photographer_Id" });
            DropTable("dbo.PhotographerAlbums");
            CreateIndex("dbo.Albums", "PhotographerId");
            AddForeignKey("dbo.Albums", "PhotographerId", "dbo.Photographers", "Id", cascadeDelete: true);
        }
    }
}
