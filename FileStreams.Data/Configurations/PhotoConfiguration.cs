using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FileStreams.Model;

namespace FileStreams.Data.Configurations
{
    public class PhotoConfiguration : EntityTypeConfiguration<Photo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoConfiguration"/> class.
        /// </summary>
        public PhotoConfiguration()
        {
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired();

            // This ensures that Entity Framework ignores the "Data" column when mapping
            Ignore(p => p.Data);
        }
    }
}
