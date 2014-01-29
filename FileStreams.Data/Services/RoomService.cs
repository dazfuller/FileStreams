using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FileStreams.Model;

namespace FileStreams.Data.Services
{
    public class RoomService : IRepository<Room>
    {
        public IList<Room> GetAll()
        {
            using (var context = new FileStreamContext())
            {
                return context.Rooms.ToList();
            }
        }

        public Room GetById(int id)
        {
            using (var context = new FileStreamContext())
            {
                var room = context.Rooms.FirstOrDefault(r => r.Id == id);
                if (room != null)
                {
                    context.Entry(room)
                        .Collection(r => r.Photos)
                        .Load();
                }
                return room;
            }
        }

        public void Update(Room entity)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Insert(Room entity)
        {
            using (var context = new FileStreamContext())
            {
                context.Rooms.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(new Room {Id = id}).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
