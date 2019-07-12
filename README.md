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



## Important notes

#### Code first workflow with [Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)

#### Implemented solution to load stores based on [SQL Server spatial data](https://docs.microsoft.com/en-us/sql/relational-databases/spatial/spatial-data-sql-server)

#### Alternative solution to load stores

1. Create SQL Server stored procedure like this
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

#### No tests coverage :disappointed:
