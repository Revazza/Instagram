using Instagram.Domain.Posts.Entities;
using Instagram.Domain.Posts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class CommentConfigurations : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {

        CommentEntityConfigurations(builder);
        CommentFieldConfigurations(builder);

    }

    private void CommentFieldConfigurations(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.CommentDescription).HasMaxLength(500).IsRequired();
    }

    private void CommentEntityConfigurations(EntityTypeBuilder<Comment> builder)
    {

        builder.HasKey(c => c.CommmentId);

        builder.Property(c => c.CommmentId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new CommentId(value));

    }
}