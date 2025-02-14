using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        void IEntityTypeConfiguration<WatchList>.Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.User)
                   .WithMany(u => u.WatchLists)
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
