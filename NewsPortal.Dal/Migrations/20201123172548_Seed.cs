using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsPortal.Dal.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Headline = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Melissa Jeltsen" },
                    { 2, "Andy McDonald" },
                    { 3, "Karen Pinchin" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Crime" },
                    { 2, "Entertainment" },
                    { 3, "Politics" },
                    { 4, "World News" },
                    { 5, "Impact" },
                    { 6, "Weird News" },
                    { 7, "Black Voices" },
                    { 8, "Women" },
                    { 9, "Comedy" },
                    { 10, "Sports" },
                    { 11, "Business" },
                    { 12, "Tech" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "Body", "CategoryId", "Headline", "PublishDate", "ShortDescription" },
                values: new object[] { 1, 1, "<div><p><span>On the evening of May 15, Amanda and Justin took the kids out for pizza. During the car ride, Amanda told Justin about her new boyfriend, that it was serious and he was planning to move in with her.  </span></p></div><div><p><span> For the past few months, Amanda had been in a long-distance relationship with Seth Richardson, whom she knew from her teen years.They, too, had met through “World of Warcraft,” and had kept in touch for a decade. </span></p></div><div><p><span>“We just had this bond, it never left,” Amanda said. “There was so much positivity when we were together.” </span></p></div>", 1, "There Were 2 Mass Shootings In Texas Last Week, But Only 1 On TV", new DateTime(2018, 5, 26, 7, 0, 0, 0, DateTimeKind.Unspecified), "She left her husband. He killed their children. Just another day in America." });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "Body", "CategoryId", "Headline", "PublishDate", "ShortDescription" },
                values: new object[] { 2, 2, "<div><p>The <a href=\"https://www.huffpost.com/topic/fifa-world-cup\">2018 FIFA World Cup</a> starts June 14 in Russia, and now it has an official song. </p></div><div><p>Producer Diplo and reggaeton star Nicky Jam collaborate on “Live It Up,” which also features Albanian singer Era Istrefi and actor Will Smith, who is trying to<a href=\"https://youtu.be/6wGj89GE6aQ\"> restart his music career</a>.</p></div><div><p>Traditionally, over the last two decades, each World Cup has had an official song.The song for the 2010 World Cup in South Africa was<a href=\"https://www.youtube.com/watch?v=pRpeEdMmmQ0\">“Waka Waka” by Shakira</a>. For the 2014 World Cup in Brazil, the song was<a href=\"https://www.youtube.com/watch?v=9W3sWiZ-iO8\">“We Are One (Ole Ola)” by Pitbull</a>.</p></div>", 2, "Will Smith Joins Diplo And Nicky Jam For The 2018 World Cup's Official Song", new DateTime(2018, 5, 26, 8, 0, 0, 0, DateTimeKind.Unspecified), "Of course it has a song." });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "Body", "CategoryId", "Headline", "PublishDate", "ShortDescription" },
                values: new object[] { 3, 3, "<div><p><span>NEGUAC, Canada ― When the harbors aren’t frozen, Maxime Daigle and his older brother Jean-Francois often take to the cold waters off New Brunswick in one of their family’s flat-bottomed boats to harvest oysters. The siblings are among hundreds of producers along this rocky coast who grow oysters for La Maison BeauSoleil in the company’s distinctive floating bags. </span></p></div>< div><p>For the Daigles this is a family business.Maxime’s father, Maurice, co-owns La Maison BeauSoleil, an oyster grower, packer and distributor that he runs with his business partner, Amédée Savoie, whose son, Allain Savoie, also works in the company.</p></div><div><p><span> When the business started growing and selling oysters from the coastal village of Neguac, at the southern end of the Acadian Peninsula, each wooden box was painstakingly packed by hand. </span></p></div><div><p><span> That was 18 years ago.La Maison BeauSoleil ― </span>called simply BeauSoleil within the seafood business ― <span> has since become Canada’s largest producer of cocktail-sized oysters. Demand for its tasty, teardrop-shaped product is at an all-time high. While the company is working hard to keep up, business as usual isn’t cutting it.</span> </p></div><div><p><span> So a new generation of robots is coming.</span></p></div><div><p><span> While details are still closely guarded, the company is developing a state-of-the-art automated sorting and packing line that will revolutionize its business.Software-controlled robots,</span> <span> able to mimic the small, complicated movements of human oyster pickers</span><strong>, </strong><span> will give the company a new competitive edge, says Savoie, and allow them to double production. </span></p></div><div><p><span> This is the type of investment and technology that has often been missing from this country’s traditional fishing economy.Many of Atlantic Canada’s rural coastal communities have been treading water since<a href=\"http://www.cbc.ca/news/canada/remembering-the-mighty-cod-fishery-20-years-after-moratorium-1.1214172\"> overfishing caused cod to collapse</a> in the 1990s, and falling further behind could signal the end for the region’s historic way of life. </span></p></div><div><p><span> But this is a family business, and the co-owners are trying to build a legacy for their children in this remote region.</span></p></div>", 5, "With Its Way Of Life At Risk, This Remote Oyster-Growing Region Called In Robots", new DateTime(2018, 5, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), "The revolution is coming to rural New Brunswick." });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NewsId",
                table: "Comments",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
