
namespace Excercise05
{
    using System;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            PhotoDbContext context = new PhotoDbContext();

            Console.WriteLine(context.Photographers.Count());

            // 08 Excercise ---------

            /* Tag tag = new Tag()
             {
                 Name = "tag of the tags 432894-320923-493423"
             };

             context.Tags.Add(tag);

             try
             {
                 context.SaveChanges();
             }
             catch (DbEntityValidationException)
             {
                 tag.Name = TagTransformer.Transform(tag.Name);

                 context.SaveChanges();
             }
             */
        }
    }
}
