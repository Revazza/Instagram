using Instagram.Domain.Stories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class StoryViewerConfigurations : IEntityTypeConfiguration<StoryViewer>
{
    public void Configure(EntityTypeBuilder<StoryViewer> builder)
    {
        builder.HasKey(x => new { x.ViewedStoryId, x.ViewerId });

        builder.HasOne(x => x.Viewer)
            .WithMany(x => x.ViewedStories)
            .HasForeignKey(x => x.ViewerId);

        builder.HasOne(x => x.ViewedStory)
            .WithMany(x => x.StoryViewers)
            .HasForeignKey(x => x.ViewedStoryId)
            .OnDelete(DeleteBehavior.ClientCascade);

    }
}