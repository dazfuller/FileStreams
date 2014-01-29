using System.Linq;
using FileStreams.Model;

namespace FileStreams.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FileStreamContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FileStreamContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Locations.AddOrUpdate(l => l.Name, new[]
            {
                new Location {Name = "Derby", Description = "It's not so bad"},
                new Location {Name = "Nottingham", Description = "Maybe go to Derby"}
            });

            context.Hotels.AddOrUpdate(h => h.Name, new[]
            {
                new Hotel {Location = context.Locations.Local.First(l => l.Name == "Derby"), Name = "Bobs Dodgy B&B"},
                new Hotel {Location = context.Locations.Local.First(l => l.Name == "Nottingham"), Name = "Rips you off"}
            });

            context.Rooms.AddOrUpdate(new[]
            {
                new Room
                {
                    Name = "The Red Room",
                    Occupancy = 4,
                    Hotel = context.Hotels.Local.First(h => h.Name == "Bobs Dodgy B&B")
                }
            });
        }
    }
}
