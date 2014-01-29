using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FileStreams.Model;

namespace FileStreams.Data.Configurations
{
    public class HotelConfiguration : EntityTypeConfiguration<Hotel>
    {
        public HotelConfiguration()
        {
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            HasMany(p => p.Rooms)
                .WithRequired(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);
        }
    }
}
