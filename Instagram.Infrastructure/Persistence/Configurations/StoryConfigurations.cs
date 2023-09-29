using Instagram.Domain.Stories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class StoryConfigurations : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.HasKey(x => x.StoryId);
        builder.Property(x => x.StoryId)
            .HasConversion(id => id.Value, value => new StoryId(value));

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Stories)
            .HasForeignKey(x => x.AuthorId);
    }
}