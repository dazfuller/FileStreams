using System;
using System.IO;
using FileStreams.Data.Services;
using FileStreams.Model;

namespace FileStreams.App
{
    class Program
    {
        static void Main()
        {
            var locationService = new LocationService();
            var locations = locationService.GetAll();

            var location = locationService.GetById(locations[0].Id);

            var hotelService = new HotelService();
            var hotel = hotelService.GetById(location.Hotels[0].Id);

            var roomService = new RoomService();
            var room = roomService.GetById(hotel.Rooms[0].Id);

            var photoService = new PhotoService();

            // Delete any existing photos
            foreach (var p in room.Photos)
            {
                photoService.Delete(p.Id);
            }

            Console.WriteLine("Adding photo to: {0}", room.Name);

            var photo = new Photo
            {
                Title = "A view of the room", Description = "A beautiful room with wonderful views of the neighbours walls", RoomId = room.Id
            };

            const string roomPhoto = @"Pictures\roomview.jpg";
            const string roomPhoto2 = @"Pictures\roomview_2.jpg";

            photo.Data = ReadPhotoData(roomPhoto);
            photoService.Insert(photo);

            photo.Data = ReadPhotoData(roomPhoto2);
            photoService.Update(photo);
        }

        private static byte[] ReadPhotoData(string roomPhoto)
        {
            using (var source = File.OpenRead(roomPhoto))
            {
                var buffer = new byte[16*1024];
                using (var ms = new MemoryStream())
                {
                    int bytesRead;
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
