using System.Data.Entity;
using FileStreams.Model;

namespace FileStreams.Data
{
    public class FileStreamContext : DbContext
    {
        public FileStreamContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            SetConfigurationOptions();
        }

        public FileStreamContext() : this("default")
        {
        }

        private void SetConfigurationOptions()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof (FileStreamContext).Assembly);
        }
    }
}
