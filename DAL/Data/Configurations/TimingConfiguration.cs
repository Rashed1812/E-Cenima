﻿using DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Configurations
{
    public class TimingConfiguration : IEntityTypeConfiguration<Timing>
    {
        public void Configure(EntityTypeBuilder<Timing> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.StartTime)
                   .IsRequired();

            builder.Property(t => t.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.Showtime)
                   .WithMany(s => s.Timings)
                   .HasForeignKey(t => t.ShowTime_Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
