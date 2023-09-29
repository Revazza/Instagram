using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

//public class StoryReactionConfigurations : IEntityTypeConfiguration<StoryReaction>
//{
//    public void Configure(EntityTypeBuilder<StoryReaction> builder)
//    {
//        builder.HasKey(x => x.StoryReactionId);
//        builder.Property(x => x.StoryReactionId)
//            .HasConversion(x => x.Value, x => new StoryReactionId(x));

//    }
//}