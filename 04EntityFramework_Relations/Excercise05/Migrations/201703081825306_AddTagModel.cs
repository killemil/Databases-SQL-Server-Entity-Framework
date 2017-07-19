namespace Excercise05.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTagModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagAlbums",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Album_AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Album_AlbumId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Album_AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagAlbums", "Album_AlbumId", "dbo.Albums");
            DropForeignKey("dbo.TagAlbums", "Tag_TagId", "dbo.Tags");
            DropIndex("dbo.TagAlbums", new[] { "Album_AlbumId" });
            DropIndex("dbo.TagAlbums", new[] { "Tag_TagId" });
            DropTable("dbo.TagAlbums");
            DropTable("dbo.Tags");
        }
    }
}
