using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeWEB.Migrations
{
    /// <inheritdoc />
    /// <inheritdoc />
    public partial class RecipeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    AllergenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Allergen__158B937FA014BFB5", x => x.AllergenID);
                });

            migrationBuilder.CreateTable(
                name: "Diets",
                columns: table => new
                {
                    DietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Diets__AB42F6BECD9C9971", x => x.DietID);
                });

            migrationBuilder.CreateTable(
                name: "KitchenTools",
                columns: table => new
                {
                    ToolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KitchenT__CC0CEBB12266E1BA", x => x.ToolID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC274802CD", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    AllergenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__BEAEB27A3F93C860", x => x.IngredientID);
                    table.ForeignKey(
                        name: "FK__Ingredien__Aller__3B40CD36",
                        column: x => x.AllergenId,
                        principalTable: "Allergens",
                        principalColumn: "AllergenID");
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Instructions = table.Column<string>(type: "text", nullable: false),
                    PrepTime = table.Column<int>(type: "int", nullable: true),
                    CookTime = table.Column<int>(type: "int", nullable: true),
                    Servings = table.Column<int>(type: "int", nullable: true),
                    ImageURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recipes__FDD988D0CD1CECE1", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK__Recipes__UserID__3493CFA7",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRecipes",
                columns: table => new
                {
                    FavoriteRecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__5CEF438B9986D8BF", x => x.FavoriteRecipeID);
                    table.ForeignKey(
                        name: "FK__FavoriteR__Recip__4E53A1AA",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__FavoriteR__UserI__4D5F7D71",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeComments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RecipeCo__C3B4DFAA86ECAAD6", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__RecipeCom__Recip__489AC854",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__RecipeCom__UserI__498EEC8D",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeDiets",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    DietID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RecipeDi__076DA7BB24B9D8E1", x => new { x.RecipeID, x.DietID });
                    table.ForeignKey(
                        name: "FK__RecipeDie__DietI__5224328E",
                        column: x => x.DietID,
                        principalTable: "Diets",
                        principalColumn: "DietID");
                    table.ForeignKey(
                        name: "FK__RecipeDie__Recip__51300E55",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RecipeIn__463363F775C2FCDB", x => new { x.RecipeID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK__RecipeIng__Ingre__3F115E1A",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__RecipeIng__Recip__3E1D39E1",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeRatings",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Review = table.Column<string>(type: "text", nullable: true),
                    RatingDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RecipeRa__FCCDF85C5FE0DE0F", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK__RecipeRat__Recip__43D61337",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__RecipeRat__UserI__44CA3770",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeTools",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    ToolID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RecipeTo__F119466B13BD6EBE", x => new { x.RecipeID, x.ToolID });
                    table.ForeignKey(
                        name: "FK__RecipeToo__Recip__55009F39",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__RecipeToo__ToolI__55F4C372",
                        column: x => x.ToolID,
                        principalTable: "KitchenTools",
                        principalColumn: "ToolID");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Allergen__737584F65FE771EC",
                table: "Allergens",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Diets__737584F6DC72AD88",
                table: "Diets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRecipes_RecipeID",
                table: "FavoriteRecipes",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRecipes_UserID",
                table: "FavoriteRecipes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_AllergenId",
                table: "Ingredients",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "UQ__Ingredie__737584F68A190DD6",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__KitchenT__737584F6BFD9949C",
                table: "KitchenTools",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeComments_RecipeID",
                table: "RecipeComments",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeComments_UserID",
                table: "RecipeComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDiets_DietID",
                table: "RecipeDiets",
                column: "DietID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientID",
                table: "RecipeIngredients",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRatings_RecipeID",
                table: "RecipeRatings",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRatings_UserID",
                table: "RecipeRatings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserID",
                table: "Recipes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTools_ToolID",
                table: "RecipeTools",
                column: "ToolID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E43A8DBE87",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053403F025B4",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteRecipes");

            migrationBuilder.DropTable(
                name: "RecipeComments");

            migrationBuilder.DropTable(
                name: "RecipeDiets");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeRatings");

            migrationBuilder.DropTable(
                name: "RecipeTools");

            migrationBuilder.DropTable(
                name: "Diets");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "KitchenTools");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
