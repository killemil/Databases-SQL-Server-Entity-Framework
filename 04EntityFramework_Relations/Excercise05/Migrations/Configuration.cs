namespace Excercise05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Models;
    using System.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<Excercise05.PhotoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Excercise05.PhotoDbContext";
        }

        protected override void Seed(Excercise05.PhotoDbContext context)
        {
            
            Photographer john = new Photographer()
            {
                Username = "Johny Bravo",
                Password = "sladn32jkn432",
                Email = "John@john.com",
                BirthDate = DateTime.ParseExact("03-04-1988", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                RegisteredOn = DateTime.Now
            };
            context.Photographers.AddOrUpdate(p => p.Username, john);
            context.SaveChanges();

            Photographer konan = new Photographer()
            {
                Username = "Konan Barbarian",
                Password = "konan1234",
                Email = "Koncheto@konan.bg",
                BirthDate = DateTime.ParseExact("04-12-1990", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                RegisteredOn = DateTime.Now
            };
            context.Photographers.AddOrUpdate(p => p.Username, konan);
            context.SaveChanges();

            Photographer boho = new Photographer()
            {
                Username = "Bohema",
                Password = "bohohoh",
                Email = "Bohho@abv.bg",
                BirthDate = DateTime.ParseExact("12-11-1980", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                RegisteredOn = DateTime.Now
            };
            context.Photographers.AddOrUpdate(p => p.Username, boho);
            context.SaveChanges();

            Picture pic1 = new Picture()
            {
                Capture = "Elk",
                Title = "Wild Elk",
                FilePath = @"c:\My Pictures\",
            };
            context.Pictures.AddOrUpdate(p => p.Title, pic1);
            context.SaveChanges();

            Picture pic2 = new Picture()
            {
                Capture = "Fish and chips",
                Title = "Deep blue",
                FilePath = @"c:\My Pictures\",
            };
            context.Pictures.AddOrUpdate(p => p.Title, pic2);
            context.SaveChanges();

            Picture pic3 = new Picture()
            {
                Capture = "Woods",
                Title = "Dark wood forest",
                FilePath = @"c:\My Pictures\",
            };
            context.Pictures.AddOrUpdate(p => p.Title, pic3);
            context.SaveChanges();

            Album albumOne = new Album()
            {
                Name = "Top of the top",
                BackgroundColor = "Dark blue",
                Pictures = new Picture[]
                {
                    pic1,
                    pic2
                },
                IsPublic = true
            };
            
            context.Albums.AddOrUpdate(a => a.Name, albumOne);

            context.SaveChanges();

            Album albumTwo = new Album()
            {
                Name = "My favorites",
                BackgroundColor = "Green",
                Pictures = new Picture[]
               {
                    pic1,
                    pic3
               },
                IsPublic = false
            };
            context.Albums.AddOrUpdate(a => a.Name, albumTwo);
            context.SaveChanges();

            Album albumThree = new Album()
            {
                Name = "Outside",
                BackgroundColor = "Grey",
                Pictures = new Picture[]
               {
                    pic1,
                    pic3,
                    pic2
               },
                IsPublic = true
            };

            context.Albums.AddOrUpdate(a => a.Name, albumThree);
            context.SaveChanges();

            Tag tag1 = new Tag()
            {
                Name = "#top",
                Albums = new Album[]
                {
                    albumOne,
                    albumThree
                }
            };
            context.Tags.AddOrUpdate(t => t.Name, tag1);
            context.SaveChanges();

            Tag tag2 = new Tag()
            {
                Name = "#canon",
                Albums = new Album[]
                {
                    albumOne,
                    albumThree,
                    albumTwo
                }
            };
            context.Tags.AddOrUpdate(t => t.Name, tag2);
            context.SaveChanges();

            Tag tag3 = new Tag()
            {
                Name = "#sea",
                Albums = new Album[]
                {
                    albumOne,
                    albumTwo
                }
            };
            context.Tags.AddOrUpdate(t => t.Name, tag3);
            context.SaveChanges();

            Tag tag4 = new Tag()
            {
                Name = "#HappyLife",
                Albums = new Album[]
                {
                    albumOne,
                    albumTwo
                }
            };
            context.Tags.AddOrUpdate(t => t.Name, tag4);
            context.SaveChanges();

            PhotographerAlbum ph1 = new PhotographerAlbum()
            {
                Album_AlbumId = albumOne.AlbumId,
                Photographer_Id = konan.Id,
                Role = Role.Owner
            };
            context.PhotographerAlbums.Add(ph1);
            context.SaveChanges();

            PhotographerAlbum ph2 = new PhotographerAlbum()
            {
                Album_AlbumId = albumTwo.AlbumId,
                Photographer_Id = john.Id,
                Role = Role.Owner
            };
            context.PhotographerAlbums.Add(ph2);
            context.SaveChanges();

            PhotographerAlbum ph3 = new PhotographerAlbum()
            {
                Album_AlbumId = albumTwo.AlbumId,
                Photographer_Id = boho.Id,
                Role = Role.Viewer
            };
            context.PhotographerAlbums.Add(ph3);
            context.SaveChanges();

        }
    }
}
