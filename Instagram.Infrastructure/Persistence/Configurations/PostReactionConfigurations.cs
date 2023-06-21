using Instagram.Domain.Posts.Entities;
using Instagram.Domain.Posts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class PostReactionConfigurations : IEntityTypeConfiguration<PostReaction>
{
    public void Configure(EntityTypeBuilder<PostReaction> builder)
    {

        builder.HasKey(pr => pr.PostReactionId);

        builder.Property(pr => pr.PostReactionId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new PostReactionId(value));

    }

    

}