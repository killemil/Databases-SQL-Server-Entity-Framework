namespace Excercise01
{
    using System.Data.Entity;

    public class LocalStoreContext : DbContext
    {
        public LocalStoreContext()
            : base("name=LocalStoreContext")
        {

        }
        public virtual DbSet<Product> Products { get; set; }
    }
}