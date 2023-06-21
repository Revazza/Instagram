using Instagram.Domain.Posts;
using Instagram.Domain.Posts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class PostConfigurations : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {

        PostEntityConfigurations(builder);
        PostFieldConfigurations(builder);

    }

    private void PostFieldConfigurations(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.PostDescription).HasMaxLength(2000).IsRequired();
    }

    private void PostEntityConfigurations(EntityTypeBuilder<Post> builder)
    {

        builder.HasKey(p => p.PostId);

        builder.Property(p => p.PostId)
            .HasConversion(id => id.Value, value => new PostId(value))
            .ValueGeneratedNever();

        builder.HasMany(p => p.PostComments)
            .WithOne(pc => pc.OriginalPost)
            .HasForeignKey(pc => pc.OriginalPostId);

        builder.HasMany(p => p.PostReactions)
            .WithOne(p => p.ReactedPost)
            .HasForeignKey(rp => rp.ReactedPostId);

    }

}