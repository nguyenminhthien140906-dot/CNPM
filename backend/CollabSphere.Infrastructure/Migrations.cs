using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using CollabSphere.Infrastructure.Persistence;

namespace CollabSphere.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("202611050001_InitialCreate")]
public class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),

                FullName = table.Column<string>(
                    maxLength: 100,
                    nullable: false),

                Email = table.Column<string>(
                    maxLength: 150,
                    nullable: false),

                Password = table.Column<string>(
                    maxLength: 255,
                    nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);

        migrationBuilder.CreateTable(
            name: "Projects",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),

                Name = table.Column<string>(
                    maxLength: 150,
                    nullable: false),

                Description = table.Column<string>(
                    maxLength: 1000,
                    nullable: true),

                OwnerId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Projects", x => x.Id);

                table.ForeignKey(
                    name: "FK_Projects_Users_OwnerId",
                    column: x => x.OwnerId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Tasks",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),

                Title = table.Column<string>(
                    maxLength: 200,
                    nullable: false),

                Status = table.Column<string>(
                    maxLength: 50,
                    nullable: false),

                ProjectId = table.Column<int>(nullable: false),

                CreatedAt = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tasks", x => x.Id);

                table.ForeignKey(
                    name: "FK_Tasks_Projects_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Projects_OwnerId",
            table: "Projects",
            column: "OwnerId");

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_ProjectId",
            table: "Tasks",
            column: "ProjectId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Tasks");

        migrationBuilder.DropTable(name: "Projects");

        migrationBuilder.DropTable(name: "Users");
    }
}
