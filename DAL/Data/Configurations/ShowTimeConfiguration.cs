using DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Configurations
{
    public class ShowTimeConfiguration : IEntityTypeConfiguration<ShowTime>
    {
        public void Configure(EntityTypeBuilder<ShowTime> builder)
        {
            builder.HasKey(s => s.Showtime_Id);
            builder.Property(s => s.Date).IsRequired();

            builder.HasOne(s => s.Cinema)
            .WithMany(c => c.Showtimes)
            .HasForeignKey(s => s.Cinema_Id)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Movie)
                   .WithMany(m => m.Showtimes) 
                   .HasForeignKey(s => s.Movie_Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
