using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizons.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixedDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destination_AspNetUsers_PublisherId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_Destination_Terrain_TerrainId",
                table: "Destination");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDestination_AspNetUsers_UserId",
                table: "UserDestination");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDestination_Destination_DestinationId",
                table: "UserDestination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDestination",
                table: "UserDestination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Terrain",
                table: "Terrain");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destination",
                table: "Destination");

            migrationBuilder.RenameTable(
                name: "UserDestination",
                newName: "UsersDestinations");

            migrationBuilder.RenameTable(
                name: "Terrain",
                newName: "Terrains");

            migrationBuilder.RenameTable(
                name: "Destination",
                newName: "Destinations");

            migrationBuilder.RenameIndex(
                name: "IX_UserDestination_DestinationId",
                table: "UsersDestinations",
                newName: "IX_UsersDestinations_DestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Destination_TerrainId",
                table: "Destinations",
                newName: "IX_Destinations_TerrainId");

            migrationBuilder.RenameIndex(
                name: "IX_Destination_PublisherId",
                table: "Destinations",
                newName: "IX_Destinations_PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersDestinations",
                table: "UsersDestinations",
                columns: new[] { "UserId", "DestinationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Terrains",
                table: "Terrains",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destinations",
                table: "Destinations",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6c2a6a1-8f57-420c-b0dd-774d8e5bd872", "AQAAAAIAAYagAAAAEGllCXAHLQ+mgwnT3t8BSA9lHNof/AxerGvHZJIbElIdU3YopjpJCGcmXhcb3CCIEQ==", "8df7601b-61c5-43f2-b6e1-18f70106b202" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2025, 8, 5, 23, 20, 50, 436, DateTimeKind.Local).AddTicks(8901));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2025, 8, 5, 23, 20, 50, 436, DateTimeKind.Local).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2025, 8, 5, 23, 20, 50, 436, DateTimeKind.Local).AddTicks(9055));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_AspNetUsers_PublisherId",
                table: "Destinations",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Terrains_TerrainId",
                table: "Destinations",
                column: "TerrainId",
                principalTable: "Terrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDestinations_AspNetUsers_UserId",
                table: "UsersDestinations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDestinations_Destinations_DestinationId",
                table: "UsersDestinations",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_AspNetUsers_PublisherId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Terrains_TerrainId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDestinations_AspNetUsers_UserId",
                table: "UsersDestinations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDestinations_Destinations_DestinationId",
                table: "UsersDestinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersDestinations",
                table: "UsersDestinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Terrains",
                table: "Terrains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destinations",
                table: "Destinations");

            migrationBuilder.RenameTable(
                name: "UsersDestinations",
                newName: "UserDestination");

            migrationBuilder.RenameTable(
                name: "Terrains",
                newName: "Terrain");

            migrationBuilder.RenameTable(
                name: "Destinations",
                newName: "Destination");

            migrationBuilder.RenameIndex(
                name: "IX_UsersDestinations_DestinationId",
                table: "UserDestination",
                newName: "IX_UserDestination_DestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Destinations_TerrainId",
                table: "Destination",
                newName: "IX_Destination_TerrainId");

            migrationBuilder.RenameIndex(
                name: "IX_Destinations_PublisherId",
                table: "Destination",
                newName: "IX_Destination_PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDestination",
                table: "UserDestination",
                columns: new[] { "UserId", "DestinationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Terrain",
                table: "Terrain",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destination",
                table: "Destination",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e71ef44-6a20-4541-93ba-b51fba6cd9e4", "AQAAAAIAAYagAAAAEBVQIVpJi/QJZ/J/8YwsjVg0h5BCrE4R36DhpgA5EOWeym+1JxjuN0pzNMVT/5qi5A==", "fc1c1de2-ea45-42f9-9f3f-b4481ebd5b79" });

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2025, 8, 5, 21, 59, 23, 678, DateTimeKind.Local).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2025, 8, 5, 21, 59, 23, 678, DateTimeKind.Local).AddTicks(9497));

            migrationBuilder.UpdateData(
                table: "Destination",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2025, 8, 5, 21, 59, 23, 678, DateTimeKind.Local).AddTicks(9499));

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_AspNetUsers_PublisherId",
                table: "Destination",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destination_Terrain_TerrainId",
                table: "Destination",
                column: "TerrainId",
                principalTable: "Terrain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDestination_AspNetUsers_UserId",
                table: "UserDestination",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDestination_Destination_DestinationId",
                table: "UserDestination",
                column: "DestinationId",
                principalTable: "Destination",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
