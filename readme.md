# [OData](https://www.odata.org)

**Links**  
* [OData](https://www.odata.org)
* [Microsoft Docs](https://docs.microsoft.com/en-us/odata)

**Notes**  
* [OData](./notes/01-odata.md)
* [Data Model](./notes/02-data-model.md)
* [URL Components](./notes/03-url-components.md)
* [Requesting Data](./notes/04-requesting-data.md)

[![odata-integration](./images/odata-integration.gif)](./images/odata-integration.gif)

## Build and Run

> These instructions assume that you have the infrastructure necessary to build and run a .NET Core 3.1 / Angular SPA. In addition, you will need Yarn and some flavor of SQL Server. You will also need to setup the connection strings in [**build.cmd**](./src/build.cmd) and [**appsettings.Development.json**](./src/ODataIntegration.Web/appsettings.Development.json) to match your environment.

1. Open a terminal resolved to the `./src` folder, and run `build.cmd`.
    * This will seed your database as well as build the project
2. Change directory to `ODataIntegration.Web` and execute `dotnet run`.

Here are some example OData queries:

* [localhost:5000/odata/Products/$count=true](http://localhost:5000/odata/Products?$count=true)
* [localhost:5000/odata/Products?$filter=Name ne 'Milk'](http://localhost:5000/odata/Products?$filter=Name%20ne%20%27Milk%27)
* [localhost:5000/odata/Products?$filter=Name in ('Coffee', 'Sugar', 'Milk')&$select=Name&$orderby=Name asc](http://localhost:5000/odata/Products?$filter=Name%20in%20(%27Coffee%27,%20%27Sugar%27,%20%27Milk%27)&$select=Name&$orderby=Name%20asc)

> See [OData Docs - Query options overview](https://docs.microsoft.com/en-us/odata/concepts/queryoptions-overview) for more details