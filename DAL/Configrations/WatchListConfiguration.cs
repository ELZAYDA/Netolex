using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        void IEntityTypeConfiguration<WatchList>.Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder
             .HasOne(w => w.User)
             .WithMany(u => u.WatchlistItems)
             .HasForeignKey(w => w.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(w => w.Movie)
                .WithMany()
                .HasForeignKey(w => w.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure each movie only appears once in a user's watchlist
            builder
                .HasIndex(w => new { w.UserId, w.MovieId })
                .IsUnique();


        }
    }
}
