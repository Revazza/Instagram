﻿using Instagram.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        UserEntityConfigurations(builder);
        UserFieldConfigurations(builder);

    }

    private void UserFieldConfigurations(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.Profile, p =>
        {
            p.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            p.Property(p => p.LastName).HasMaxLength(80).IsRequired();
            p.Property(p => p.Age).IsRequired();
        });

    }

    private void UserEntityConfigurations(EntityTypeBuilder<User> builder)
    {

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.HasMany(u => u.Posts)
           .WithOne(p => p.PostAuthor)
           .HasForeignKey(p => p.PostAuthorId)
           .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(p => p.PostReactions)
            .WithOne(p => p.ReactionAuthor)
            .HasForeignKey(p => p.ReactionAuthorId);

        builder.HasMany(u => u.UserComments)
            .WithOne(ur => ur.CommentAuthor)
            .HasForeignKey(u => u.CommentAuthorId);

        builder.HasMany(u => u.Followers)
            .WithOne(f => f.FollowedUser)
            .HasForeignKey(f => f.FollowedUserId);

        builder.HasMany(u => u.Followings)
            .WithOne(f => f.UserFollowing)
            .HasForeignKey(f => f.UserFollowingId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(u => u.Friends)
            .WithOne(f => f.Friend)
            .HasForeignKey(f => f.FriendId);

    }

}