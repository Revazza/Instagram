using Instagram.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class FriendShipConfigurations : IEntityTypeConfiguration<FriendShip>
{
    public void Configure(EntityTypeBuilder<FriendShip> builder)
    {

        FriendShipEntityConfigurations(builder);

    }

    private void FriendShipEntityConfigurations(EntityTypeBuilder<FriendShip> builder)
    {
        builder.HasKey(f => f.FriendShipId);
        builder.Property(f => f.FriendShipId)
            .HasConversion(id => id.Value, value => new FriendShipId(value));


    }
}