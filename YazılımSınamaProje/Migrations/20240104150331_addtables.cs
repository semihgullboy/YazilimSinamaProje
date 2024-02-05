using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazılımSınamaProje.Migrations
{
    /// <inheritdoc />
    public partial class addtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    money = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    projectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectBudget = table.Column<int>(type: "int", nullable: false),
                    projectTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.projectID);
                    table.ForeignKey(
                        name: "FK_projects_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "offers",
                columns: table => new
                {
                    offerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bidderID = table.Column<int>(type: "int", nullable: false),
                    offerRecipientID = table.Column<int>(type: "int", nullable: false),
                    bidAmount = table.Column<int>(type: "int", nullable: false),
                    projectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offers", x => x.offerID);
                    table.ForeignKey(
                        name: "FK_offers_projects_projectID",
                        column: x => x.projectID,
                        principalTable: "projects",
                        principalColumn: "projectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    workID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workBusinessID = table.Column<int>(type: "int", nullable: false),
                    employerID = table.Column<int>(type: "int", nullable: false),
                    projectID = table.Column<int>(type: "int", nullable: false),
                    newBudget = table.Column<int>(type: "int", nullable: false),
                    explanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.workID);
                    table.ForeignKey(
                        name: "FK_works_projects_projectID",
                        column: x => x.projectID,
                        principalTable: "projects",
                        principalColumn: "projectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_offers_projectID",
                table: "offers",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_projects_userId",
                table: "projects",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_works_projectID",
                table: "works",
                column: "projectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offers");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
