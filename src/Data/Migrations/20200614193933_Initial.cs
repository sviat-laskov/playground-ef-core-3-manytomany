using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Courses",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Title = table.Column<string>(maxLength: 30)
                },
                constraints: table => { table.PrimaryKey("PK_Courses", x => x.Id); });

            migrationBuilder.CreateTable("Students",
                table => new
                {
                    Id = table.Column<Guid>(),
                    FirstName = table.Column<string>(maxLength: 30),
                    LastName = table.Column<string>(maxLength: 30)
                },
                constraints: table => { table.PrimaryKey("PK_Students", x => x.Id); });

            migrationBuilder.CreateTable("CourseStudents",
                table => new
                {
                    CourseId = table.Column<Guid>(),
                    StudentId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudents", x => new
                    {
                        x.StudentId,
                        x.CourseId
                    });
                    table.ForeignKey("FK_CourseStudents_Courses_CourseId",
                        x => x.CourseId,
                        "Courses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_CourseStudents_Students_StudentId",
                        x => x.StudentId,
                        "Students",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData("Courses",
                new[] {"Id", "Title"},
                new object[,] {{new Guid("30fd7c5b-8181-4cc7-8f4d-58a122c077d0"), "Math"}, {new Guid("d6221796-b017-4bd2-b7c8-bf59b7e2cd41"), "Programming"}});

            migrationBuilder.InsertData("Students",
                new[] {"Id", "FirstName", "LastName"},
                new object[,] {{new Guid("9505979e-3f4b-4f20-88ce-9a3069fa6d51"), "Sviat", "Laskov"}, {new Guid("a461a090-434c-414a-a188-295207a10b04"), "Ivan", "Petrov"}});

            migrationBuilder.InsertData("CourseStudents",
                new[] {"StudentId", "CourseId"},
                new object[,] {{new Guid("9505979e-3f4b-4f20-88ce-9a3069fa6d51"), new Guid("d6221796-b017-4bd2-b7c8-bf59b7e2cd41")}, {new Guid("a461a090-434c-414a-a188-295207a10b04"), new Guid("30fd7c5b-8181-4cc7-8f4d-58a122c077d0")}, {new Guid("a461a090-434c-414a-a188-295207a10b04"), new Guid("d6221796-b017-4bd2-b7c8-bf59b7e2cd41")}});

            migrationBuilder.CreateIndex("IX_CourseStudents_CourseId",
                "CourseStudents",
                "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CourseStudents");

            migrationBuilder.DropTable("Courses");

            migrationBuilder.DropTable("Students");
        }
    }
}