using Instagram.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class ChatConfigurations : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {

        ChatEntityConfigurations(builder);
        ChatFieldConfigurations(builder);

    }

    private void ChatFieldConfigurations(EntityTypeBuilder<Chat> builder)
    {
        builder.Property(c => c.ChatName).HasMaxLength(30);
    }

    private void ChatEntityConfigurations(EntityTypeBuilder<Chat> builder)
    {

        builder.HasKey(c => c.ChatId);
        builder.Property(c => c.ChatId)
            .HasConversion(id => id.Value, value => new ChatId(value));

        builder.HasMany(c => c.Participants)
            .WithMany(p => p.Chats)
            .UsingEntity(u => u.ToTable("ChatParticipants"));

        builder.HasMany(c => c.ChatMessages)
            .WithOne(cm => cm.OriginalChat)
            .HasForeignKey(c => c.OriginalChatId);

    }
}