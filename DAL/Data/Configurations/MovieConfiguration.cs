using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DAL.Data.Models.Movie_Module;

namespace DAL.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.ImageURL).IsRequired();
            builder.Property(m => m.ReleaseDate).IsRequired();
            builder.Property(m => m.MovieCategory).IsRequired();

            builder.HasOne(m => m.Producer)
                   .WithMany(p => p.Movies)
                   .HasForeignKey(m => m.ProducerId);

            builder.HasMany(m => m.MovieActors)
                   .WithOne(ma => ma.Movie)
                   .HasForeignKey(ma => ma.MovieId);

            builder.HasMany(m => m.Showtimes)
                   .WithOne(s => s.Movie)
                   .HasForeignKey(s => s.Movie_Id);
        }
    }
}
