using DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.ProfilePictureURL).IsRequired();
            builder.Property(a => a.FullName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Bio).IsRequired();

            builder.HasMany(a => a.MovieActors)
                   .WithOne(ma => ma.Actor)
                   .HasForeignKey(ma => ma.ActorId);
        }
    }
}
