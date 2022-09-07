using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.EntityFramework.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "Price", "Deleted" },
                values: new object[,]
                {
                    { 1, "Web application development in .Net", 100m, false },
                    { 2, "Online couching", 200m,false }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Subject", "Deleted" },
                values: new object[,]
                {
                    { 1, 1, "Web architecture",false },
                    { 2, 1, "Data access layer",false },
                    { 3, 2, "Couch activities",false },
                    { 4, 2, "Public speaking: presentation and speech",false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
