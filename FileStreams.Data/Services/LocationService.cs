using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FileStreams.Model;

namespace FileStreams.Data.Services
{
    public class LocationService : IRepository<Location>
    {
        public IList<Location> GetAll()
        {
            using (var context = new FileStreamContext())
            {
                return context.Locations.ToList();
            }
        }

        public Location GetById(int id)
        {
            using (var context = new FileStreamContext())
            {
                var location = context.Locations.FirstOrDefault(l => l.Id == id);
                if (location != null)
                {
                    context.Entry(location)
                        .Collection(l => l.Hotels)
                        .Load();
                }
                return location;
            }
        }

        public void Update(Location entity)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Insert(Location entity)
        {
            using (var context = new FileStreamContext())
            {
                context.Locations.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(new Location { Id = id}).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
