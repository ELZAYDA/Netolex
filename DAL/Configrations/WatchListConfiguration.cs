using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace DAL.Configurations
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        void IEntityTypeConfiguration<WatchList>.Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.HasKey(w => w.Id);

           


            builder.Property(w => w.Status)
                .HasConversion(new EnumToStringConverter<WatchStatus>()); 
            //.HasConversion(
            //v => v.ToString(),           // تحويل Enum إلى String عند الحفظ
            //v => (WatchStatus)Enum.Parse(typeof(WatchStatus), v)  // تحويل String إلى Enum عند القراءةكنص وليس كرقم
        }
    }
}
