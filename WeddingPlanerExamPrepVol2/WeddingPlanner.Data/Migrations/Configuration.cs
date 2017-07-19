namespace WeddingPlanner.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeddingPlanner.Data.WeddingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WeddingPlanner.Data.WeddingContext";
        }

        protected override void Seed(WeddingPlanner.Data.WeddingContext context)
        {
        }
    }
}
