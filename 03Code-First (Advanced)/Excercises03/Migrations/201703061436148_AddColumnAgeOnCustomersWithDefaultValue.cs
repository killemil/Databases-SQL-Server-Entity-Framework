namespace Excercises03.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddColumnAgeOnCustomersWithDefaultValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Age", c => c.Int(nullable: false, defaultValue: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Age");
        }
    }
}
