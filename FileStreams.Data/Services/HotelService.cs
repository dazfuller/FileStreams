using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FileStreams.Model;

namespace FileStreams.Data.Services
{
    public class HotelService : IRepository<Hotel>
    {
        public IList<Hotel> GetAll()
        {
            using (var context = new FileStreamContext())
            {
                return context.Hotels.ToList();
            }
        }

        public Hotel GetById(int id)
        {
            using (var context = new FileStreamContext())
            {
                var hotel = context.Hotels.FirstOrDefault(h => h.Id == id);
                if (hotel != null)
                {
                    context.Entry(hotel)
                        .Reference(h => h.Location)
                        .Load();

                    context.Entry(hotel)
                        .Collection(h => h.Rooms)
                        .Load();
                }
                return hotel;
            }
        }

        public void Update(Hotel entity)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Insert(Hotel entity)
        {
            using (var context = new FileStreamContext())
            {
                context.Hotels.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new FileStreamContext())
            {
                context.Entry(new Hotel { Id = id}).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
