namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelfReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Book_Id", "dbo.Books");
            DropIndex("dbo.Books", new[] { "Book_Id" });
            CreateTable(
                "dbo.RelatedBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        RelatedBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.RelatedBookId })
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Books", t => t.RelatedBookId)
                .Index(t => t.BookId)
                .Index(t => t.RelatedBookId);
            
            DropColumn("dbo.Books", "Book_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Book_Id", c => c.Int());
            DropForeignKey("dbo.RelatedBooks", "RelatedBookId", "dbo.Books");
            DropForeignKey("dbo.RelatedBooks", "BookId", "dbo.Books");
            DropIndex("dbo.RelatedBooks", new[] { "RelatedBookId" });
            DropIndex("dbo.RelatedBooks", new[] { "BookId" });
            DropTable("dbo.RelatedBooks");
            CreateIndex("dbo.Books", "Book_Id");
            AddForeignKey("dbo.Books", "Book_Id", "dbo.Books", "Id");
        }
    }
}
