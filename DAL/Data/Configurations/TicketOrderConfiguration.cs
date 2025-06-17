using DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Configurations
{
    public class TicketOrderConfiguration : IEntityTypeConfiguration<TicketOrder>
    {
        public void Configure(EntityTypeBuilder<TicketOrder> builder)
        {
            builder.HasKey(o => o.Order_Id);
        }
    }
}
