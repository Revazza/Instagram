using Instagram.Domain.Chats.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class MessageConfigurations : IEntityTypeConfiguration<Message>
{

    public void Configure(EntityTypeBuilder<Message> builder)
    {

        MessageEntityConfigurations(builder);
        MessageFieldConfigurations(builder);

    }

    private void MessageFieldConfigurations(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.MessageText).HasMaxLength(255).IsRequired();
    }

    private void MessageEntityConfigurations(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.MessageId);
        builder.Property(m => m.MessageId)
            .HasConversion(id => id.Value, value => new MessageId(value));

        builder.HasOne(m => m.Sender)
            .WithMany(s => s.Messages)
            .HasForeignKey(m => m.SenderId);

    }
}