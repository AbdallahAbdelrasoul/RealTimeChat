using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Messages;

namespace RealTimeChat.DataAccess.Mappings
{
    public class MessagesDbMapping : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages").HasKey(k => k.Id);

            builder.Property(x => x.IsSeen).HasDefaultValue(false);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Ignore(x => x.Validator);
        }
    }
}
