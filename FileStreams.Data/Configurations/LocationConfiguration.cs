using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FileStreams.Model;

namespace FileStreams.Data.Configurations
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(500);

            HasMany(p => p.Hotels)
                .WithRequired(h => h.Location)
                .HasForeignKey(h => h.LocationId);
        }
    }
}
