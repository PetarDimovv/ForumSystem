using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class _3rdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "IsBlocked", "IsLoggedIn", "LastName", "Password", "Phone", "Role", "Username" },
                values: new object[] { 1, null, null, false, false, false, null, null, null, 0, "Aleks77" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "IsBlocked", "IsLoggedIn", "LastName", "Password", "Phone", "Role", "Username" },
                values: new object[] { 2, null, null, false, false, false, null, null, null, 0, "PeshoEQk" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "IsBlocked", "IsLoggedIn", "LastName", "Password", "Phone", "Role", "Username" },
                values: new object[] { 3, null, null, true, false, false, null, null, null, 0, "Piotri" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommentCount", "Content", "Likes", "Title", "UserId" },
                values: new object[] { 1, 0, "Try to follow this guide *Link* ", 0, "How to create a SQL server", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommentCount", "Content", "Likes", "Title", "UserId" },
                values: new object[] { 3, 0, "Try to follow this guide *LInk* ", 0, "How to update a SQL SErver", 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommentCount", "Content", "Likes", "Title", "UserId" },
                values: new object[] { 2, 0, "Try to follow this guide *link* ", 0, "How to delete a SQL Server", 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "Likes", "PostId", "UserId" },
                values: new object[] { 1, "I Don't see this link. Can you send me here.", 0, 1, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "Likes", "PostId", "UserId" },
                values: new object[] { 3, "I don'T see this link. Can you send me here.", 0, 3, 3 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "Likes", "PostId", "UserId" },
                values: new object[] { 2, "I don't See this link. Can you send me here.", 0, 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
