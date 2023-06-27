using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Profile_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Profile_LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Profile_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profile_Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ChatParticipants",
                columns: table => new
                {
                    ChatsChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipants", x => new { x.ChatsChatId, x.ParticipantsUserId });
                    table.ForeignKey(
                        name: "FK_ChatParticipants_Chat_ChatsChatId",
                        column: x => x.ChatsChatId,
                        principalTable: "Chat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_Users_ParticipantsUserId",
                        column: x => x.ParticipantsUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    FollowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFollowingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK_Followers_Users_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Followers_Users_UserFollowingId",
                        column: x => x.UserFollowingId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "FriendShip",
                columns: table => new
                {
                    FriendShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FriendId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendShip", x => x.FriendShipId);
                    table.ForeignKey(
                        name: "FK_FriendShip_Users_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Chat_OriginalChatId",
                        column: x => x.OriginalChatId,
                        principalTable: "Chat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostAuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalReactions = table.Column<int>(type: "int", nullable: false),
                    TotalComments = table.Column<int>(type: "int", nullable: false),
                    PostDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_PostAuthorId",
                        column: x => x.PostAuthorId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentAuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommmentId);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_OriginalPostId",
                        column: x => x.OriginalPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentAuthorId",
                        column: x => x.CommentAuthorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    PostReactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactedPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactionAuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => x.PostReactionId);
                    table.ForeignKey(
                        name: "FK_PostReactions_Posts_ReactedPostId",
                        column: x => x.ReactedPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReactions_Users_ReactionAuthorId",
                        column: x => x.ReactionAuthorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_ParticipantsUserId",
                table: "ChatParticipants",
                column: "ParticipantsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAuthorId",
                table: "Comments",
                column: "CommentAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OriginalPostId",
                table: "Comments",
                column: "OriginalPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_FollowedUserId",
                table: "Followers",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_UserFollowingId",
                table: "Followers",
                column: "UserFollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendShip_FriendId",
                table: "FriendShip",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_OriginalChatId",
                table: "Message",
                column: "OriginalChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_ReactedPostId",
                table: "PostReactions",
                column: "ReactedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_ReactionAuthorId",
                table: "PostReactions",
                column: "ReactionAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostAuthorId",
                table: "Posts",
                column: "PostAuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatParticipants");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "FriendShip");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
