namespace Excercises03.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesAddDateDefaultValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "Date", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "Date", c => c.DateTime(defaultValueSql: "NULL"));
        }
    }
}
