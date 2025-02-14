using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configrations
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Title); // إضافة فهرس لتحسين الأداء
            builder.Property(m => m.Title).IsRequired().HasMaxLength(255);

        // علاقات One-to-Many
        builder.HasMany(m => m.Reviews)
                   .WithOne(r => r.Movie)
                   .HasForeignKey(r => r.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
