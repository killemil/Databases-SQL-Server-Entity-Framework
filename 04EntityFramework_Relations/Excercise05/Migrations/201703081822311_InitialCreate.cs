namespace Excercise05.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BackgroundColor = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        PhotographerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.Photographers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        RegisteredOn = c.DateTime(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Capture = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.PictureId);
            
            CreateTable(
                "dbo.PictureAlbums",
                c => new
                    {
                        Picture_PictureId = c.Int(nullable: false),
                        Album_AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Picture_PictureId, t.Album_AlbumId })
                .ForeignKey("dbo.Pictures", t => t.Picture_PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumId, cascadeDelete: true)
                .Index(t => t.Picture_PictureId)
                .Index(t => t.Album_AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PictureAlbums", "Album_AlbumId", "dbo.Albums");
            DropForeignKey("dbo.PictureAlbums", "Picture_PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Albums", "PhotographerId", "dbo.Photographers");
            DropIndex("dbo.PictureAlbums", new[] { "Album_AlbumId" });
            DropIndex("dbo.PictureAlbums", new[] { "Picture_PictureId" });
            DropIndex("dbo.Albums", new[] { "PhotographerId" });
            DropTable("dbo.PictureAlbums");
            DropTable("dbo.Pictures");
            DropTable("dbo.Photographers");
            DropTable("dbo.Albums");
        }
    }
}
