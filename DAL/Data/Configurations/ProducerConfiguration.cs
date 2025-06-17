using DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Configurations
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProfilePictureURL).IsRequired();
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Bio).IsRequired();
        }
    }
}
