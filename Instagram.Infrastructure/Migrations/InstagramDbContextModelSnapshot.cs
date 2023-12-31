﻿// <auto-generated />
using System;
using Instagram.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Instagram.Infrastructure.Migrations
{
    [DbContext(typeof(InstagramDbContext))]
    partial class InstagramDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<Guid>("ChatsChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParticipantsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ChatsChatId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("ChatParticipants", (string)null);
                });

            modelBuilder.Entity("Instagram.Domain.Chats.Chat", b =>
                {
                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChatName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime2");

                    b.HasKey("ChatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Instagram.Domain.Chats.Entities.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("OriginalChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("OriginalChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Instagram.Domain.Followers.Follower", b =>
                {
                    b.Property<Guid>("FollowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FollowedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserFollowingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FollowId");

                    b.HasIndex("FollowedUserId");

                    b.HasIndex("UserFollowingId");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Entities.Comment", b =>
                {
                    b.Property<Guid>("CommmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentAuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("OriginalPostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommmentId");

                    b.HasIndex("CommentAuthorId");

                    b.HasIndex("OriginalPostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Entities.PostReaction", b =>
                {
                    b.Property<Guid>("PostReactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReactedPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReactionAuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PostReactionId");

                    b.HasIndex("ReactedPostId");

                    b.HasIndex("ReactionAuthorId");

                    b.ToTable("PostReactions");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Post", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostAuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostDescription")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("TotalComments")
                        .HasColumnType("int");

                    b.Property<int>("TotalReactions")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("PostAuthorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Instagram.Domain.Stories.Entities.StoryViewer", b =>
                {
                    b.Property<Guid>("ViewedStoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ViewerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ViewedStoryId", "ViewerId");

                    b.HasIndex("ViewerId");

                    b.ToTable("StoryViewer");
                });

            modelBuilder.Entity("Instagram.Domain.Stories.Story", b =>
                {
                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<byte[]>("MediaData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("MediaType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("StoryId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("Instagram.Domain.Users.Entities.FriendShip", b =>
                {
                    b.Property<Guid>("FriendShipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FriendShipId");

                    b.HasIndex("FriendId");

                    b.ToTable("FriendShip");
                });

            modelBuilder.Entity("Instagram.Domain.Users.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Instagram.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<Instagram.Domain.Users.UserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<Instagram.Domain.Users.UserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<Instagram.Domain.Users.UserId>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<Instagram.Domain.Users.UserId>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<Instagram.Domain.Users.UserId>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.HasOne("Instagram.Domain.Chats.Chat", null)
                        .WithMany()
                        .HasForeignKey("ChatsChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Instagram.Domain.Chats.Entities.Message", b =>
                {
                    b.HasOne("Instagram.Domain.Chats.Chat", "OriginalChat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("OriginalChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Users.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OriginalChat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Instagram.Domain.Followers.Follower", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", "FollowedUser")
                        .WithMany("Followers")
                        .HasForeignKey("FollowedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Users.User", "UserFollowing")
                        .WithMany("Followings")
                        .HasForeignKey("UserFollowingId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("FollowedUser");

                    b.Navigation("UserFollowing");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Entities.Comment", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", "CommentAuthor")
                        .WithMany("UserComments")
                        .HasForeignKey("CommentAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Posts.Post", "OriginalPost")
                        .WithMany("PostComments")
                        .HasForeignKey("OriginalPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentAuthor");

                    b.Navigation("OriginalPost");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Entities.PostReaction", b =>
                {
                    b.HasOne("Instagram.Domain.Posts.Post", "ReactedPost")
                        .WithMany("PostReactions")
                        .HasForeignKey("ReactedPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Users.User", "ReactionAuthor")
                        .WithMany("PostReactions")
                        .HasForeignKey("ReactionAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReactedPost");

                    b.Navigation("ReactionAuthor");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Post", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", "PostAuthor")
                        .WithMany("Posts")
                        .HasForeignKey("PostAuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PostAuthor");
                });

            modelBuilder.Entity("Instagram.Domain.Stories.Entities.StoryViewer", b =>
                {
                    b.HasOne("Instagram.Domain.Stories.Story", "ViewedStory")
                        .WithMany("StoryViewers")
                        .HasForeignKey("ViewedStoryId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Users.User", "Viewer")
                        .WithMany("ViewedStories")
                        .HasForeignKey("ViewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ViewedStory");

                    b.Navigation("Viewer");
                });

            modelBuilder.Entity("Instagram.Domain.Stories.Story", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", "Author")
                        .WithMany("Stories")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Instagram.Domain.Users.Entities.FriendShip", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", "Friend")
                        .WithMany("Friends")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Friend");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<Instagram.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Instagram.Domain.Users.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<Instagram.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<Instagram.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<Instagram.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Instagram.Domain.Users.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Instagram.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<Instagram.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Instagram.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Instagram.Domain.Chats.Chat", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("Instagram.Domain.Posts.Post", b =>
                {
                    b.Navigation("PostComments");

                    b.Navigation("PostReactions");
                });

            modelBuilder.Entity("Instagram.Domain.Stories.Story", b =>
                {
                    b.Navigation("StoryViewers");
                });

            modelBuilder.Entity("Instagram.Domain.Users.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Friends");

                    b.Navigation("Messages");

                    b.Navigation("PostReactions");

                    b.Navigation("Posts");

                    b.Navigation("Stories");

                    b.Navigation("UserComments");

                    b.Navigation("ViewedStories");
                });
#pragma warning restore 612, 618
        }
    }
}
