using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sl_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    weight = table.Column<short>(nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sl_types", x => x.id);
                    table.UniqueConstraint("AK_sl_types_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "sl_stores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    type_id = table.Column<int>(nullable: true),
                    lat = table.Column<double>(nullable: true),
                    lng = table.Column<double>(nullable: true),
                    name = table.Column<string>(type: "varchar(100)", nullable: true),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: true),
                    address3 = table.Column<string>(type: "varchar(255)", nullable: true),
                    city = table.Column<string>(type: "varchar(100)", nullable: true),
                    state = table.Column<string>(type: "varchar(50)", nullable: true),
                    postal_code = table.Column<string>(type: "varchar(20)", nullable: true),
                    country = table.Column<string>(type: "varchar(50)", nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    booking_url = table.Column<string>(type: "varchar(255)", nullable: true),
                    mon_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    mon_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    tue_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    tue_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    wed_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    wed_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    thu_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    thu_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    fri_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    fri_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    sat_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    sat_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    sun_hrs_o = table.Column<string>(type: "varchar(20)", nullable: true),
                    sun_hrs_c = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "getdate()"),
                    location = table.Column<IPoint>(nullable: true, computedColumnSql: "case when [lat] is null or [lng] is null then null else geography::Point([lat], [lng], 4326) end persisted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sl_stores", x => x.id);
                    table.ForeignKey(
                        name: "FK_sl_stores_sl_types_type_id",
                        column: x => x.type_id,
                        principalTable: "sl_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sl_stores_type_id",
                table: "sl_stores",
                column: "type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sl_stores");

            migrationBuilder.DropTable(
                name: "sl_types");
        }
    }
}
