using Instagram.Domain.Followers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class FollowerConfigurations : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder.HasKey(f => f.FollowId);

        builder.Property(p => p.FollowId)
            .HasConversion(id => id.Value, value => new FollowId(value));

       

    }
}