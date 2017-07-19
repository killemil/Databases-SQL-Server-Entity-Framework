namespace Excercise01.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLicenseModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        LicenseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ResourceId = c.Int(),
                    })
                .PrimaryKey(t => t.LicenseId)
                .ForeignKey("dbo.Resources", t => t.ResourceId)
                .Index(t => t.ResourceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Licenses", "Resource_ResourceId", "dbo.Resources");
            DropIndex("dbo.Licenses", new[] { "Resource_ResourceId" });
            DropTable("dbo.Licenses");
        }
    }
}
