using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class MovieDirectorConfiguration : IEntityTypeConfiguration<MovieDirector>
    {
        void IEntityTypeConfiguration<MovieDirector>.Configure(EntityTypeBuilder<MovieDirector> builder)
        {
            builder.HasKey(md => new { md.MovieId, md.DirectorId });

            builder.HasOne(md => md.Movie)
                   .WithMany(m => m.MovieDirectors)
                   .HasForeignKey(md => md.MovieId);

            builder.HasOne(md => md.Director)
                   .WithMany(d => d.MovieDirectors)
                   .HasForeignKey(md => md.DirectorId);
        }
    }
}
