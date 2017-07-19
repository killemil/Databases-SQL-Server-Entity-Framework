namespace WeddingPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForgottenCapacityProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "Capacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Venues", "Capacity");
        }
    }
}
