# store-locator

## Table of contents
- [Required software](#required-software)
- [Prebuilt packages](#prebuilt-packages)
- [Setup instructions](#setup-instructions)
- [Important notes](#important-notes)

## Required software

- [Microsoft .NET Core 2.2](https://dotnet.microsoft.com/download)
- [Microsoft SQL Server 13.0.4001.0 or higher](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Microsoft Visual Studio 2019](https://visualstudio.microsoft.com/)

## Prebuilt packages

- [Windows x64](https://guidance1-my.sharepoint.com/:u:/g/personal/anton_burkovsky_guidance_com/EQdCMse2LcNEmrBcwth0zEYBigjaELv2qr1FmAQIWFQSMg?e=LMwUPD)
- [macOS x64](https://guidance1-my.sharepoint.com/:u:/g/personal/anton_burkovsky_guidance_com/ERZr5oI0bAlLhL17oTiCE8gBpBQ5QfCT6V9UglBjT9x7UA?e=GVfj03)

## Setup instructions

**Using prebuilt packages**

- Unpack and run `API.exe` file
- Application should be available at http://localhost:5000/

**Using Microsoft Visual Studio**

- Open `StoreLocator.sln`
- From the main menu select `Debug -> Start Without Debugging (Ctrl + F5)`
- Browser should automatically load the http://localhost:5000/swagger/index.html page

**Both**

- Database `StoreLocator` should be created automatically on the application startup
- [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) was used for simplicity. Update `appsettings.json` file by changing `DefaultConnection` value if another database should be used
- Use the following SQL Server scripts to insert some testing data:
```
USE [StoreLocator]
GO

INSERT [dbo].[sl_types] ([name], [weight]) VALUES (N'Type 1', 10)
INSERT [dbo].[sl_types] ([name], [weight]) VALUES (N'Type 2', 20)
INSERT [dbo].[sl_types] ([name], [weight]) VALUES (N'Type 3', 30)
GO

INSERT [dbo].[sl_stores] ([type_id], [lat], [lng], [name], [address1], [address2], [address3], [city], [state], [postal_code], [country]) VALUES (1, 37.7857182, -122.4010508, N'Store 1', N'Address 1', N'Address 2', N'Address 3', N'Los Angeles', N'CA', NULL, N'US')
INSERT [dbo].[sl_stores] ([type_id], [lat], [lng], [name], [address1], [address2], [address3], [city], [state], [postal_code], [country]) VALUES (2, 38.100626, -122.698255, N'Store 2', N'Address 1', N'Address 2', N'Address 3', N'Marine', N'CA', NULL, N'US')
INSERT [dbo].[sl_stores] ([type_id], [lat], [lng], [name], [address1], [address2], [address3], [city], [state], [postal_code], [country]) VALUES (2, 35.319208, -119.116885, N'Store 3', N'Address 1', N'Address 2', N'Address 3', N'Bakersfield', N'CA', NULL, N'US')
INSERT [dbo].[sl_stores] ([type_id], [lat], [lng], [name], [address1], [address2], [address3], [city], [state], [postal_code], [country]) VALUES (2, 29.813233, -95.540026, N'Store 4', N'Address 1', N'Address 2', N'Address 3', N'Houston', N'TX', NULL, N'US')
INSERT [dbo].[sl_stores] ([type_id], [lat], [lng], [name], [address1], [address2], [address3], [city], [state], [postal_code], [country]) VALUES (3, 40.714344, -73.914189, N'Store 5', N'Address 1', N'Address 2', N'Address 3', N'New York', N'NY', NULL, N'US')
GO
```
- Endpoint should be available at **/find-stores**
- [Swagger](https://swagger.io/) should be available at **/swagger**
- If corresponding SSL certificates are preinstalled in the system all requests should be redirected from http://localhost:5000/ to https://localhost:5001/

## Important notes

**Code first workflow with [migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)**

**Implemented solution to load stores based on [SQL Server spatial data](https://docs.microsoft.com/en-us/sql/relational-databases/spatial/spatial-data-sql-server)**

**Alternative solution to load stores**

1. Create SQL Server stored procedure like this:
```
use StoreLocator
go

create procedure GetStores
	@lat float,
	@lng float,
	@radius int
as
begin
	declare @p geography = geography::Point(@lat, @lng, 4326)

	select * from (select *, ss.location.STDistance(@p) as distance from sl_stores ss) as ess
	left join sl_types as st on st.id = ess.type_id
	where ess.distance <= @radius * 1.609 * 1000
	order by st.weight desc, distance asc
end
go
```
2. Use [raw SQL query](https://docs.microsoft.com/en-us/ef/core/querying/raw-sql)

With this approach `STDistance` function gets called only once (which theoretically could improve performance)

**No tests coverage :disappointed:**
