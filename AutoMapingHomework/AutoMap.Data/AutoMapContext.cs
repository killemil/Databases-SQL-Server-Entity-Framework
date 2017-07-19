namespace AutoMap.Data
{
    using Models;
    using System.Data.Entity;

    public class AutoMapContext : DbContext
    {
        public AutoMapContext()
            : base("name=AutoMapContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}