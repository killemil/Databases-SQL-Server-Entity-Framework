namespace WeddingPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMappingTableNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WeddingVenues", name: "VenueId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.WeddingVenues", name: "WeddingId", newName: "VenueId");
            RenameColumn(table: "dbo.WeddingVenues", name: "__mig_tmp__0", newName: "WeddingId");
            RenameIndex(table: "dbo.WeddingVenues", name: "IX_VenueId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.WeddingVenues", name: "IX_WeddingId", newName: "IX_VenueId");
            RenameIndex(table: "dbo.WeddingVenues", name: "__mig_tmp__0", newName: "IX_WeddingId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.WeddingVenues", name: "IX_WeddingId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.WeddingVenues", name: "IX_VenueId", newName: "IX_WeddingId");
            RenameIndex(table: "dbo.WeddingVenues", name: "__mig_tmp__0", newName: "IX_VenueId");
            RenameColumn(table: "dbo.WeddingVenues", name: "WeddingId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.WeddingVenues", name: "VenueId", newName: "WeddingId");
            RenameColumn(table: "dbo.WeddingVenues", name: "__mig_tmp__0", newName: "VenueId");
        }
    }
}
