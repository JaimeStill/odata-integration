# Modifying Data

## Create an Entity

To create an entity in a collection, the client sends a POST request to that collection's URL. The POST body MUST contain a single valid entity representation. The request below creates a `Person` which contains complex type and collection properties:

**HTTP POST Request**  

``` json
POST serviceRoot/People
    OData-Version: 4.0
    Content-Type: application/json;odata.metadata=minimal
    Accept: application/json
{
    "@odata.type" : "Microsoft.OData.SampleService.Models.TripPin.Person",
    "UserName": "teresa",
    "FirstName" : "Teresa",
    "LastName" : "Gilbert",
    "Gender" : "Female",
    "Emails" : ["teresa@example.com", "teresa@contoso.com"],
    "AddressInfo" : [{
        "Address" : "1 Suffolk Ln.",
        "City" : {
            "CountryRegion" : "United States",
            "Name" : "Boise",
            "Region" : "ID"
        }
    }]
}
```

**Response Payload**

```json
HTTP/1.1 201 Created
    Content-Length: 468
    Content-Type: application/json;odata.metadata=minimal;odata.streaming=true;IEEE754Compatible=false;charset=utf-8
    Location: serviceRoot/People('teresa')
    OData-Version: 4.0
{
    "@odata.context":"serviceRoot/$metadata#People/$entity",
    "@odata.id":"serviceRoot/People('teresa')",
    "@odata.editLink":"serviceRoot/People('teresa')",
    "UserName":"teresa",
    "FirstName":"Teresa",
    "LastName":"Gilbert",
    "Emails":["teresa@example.com","teresa@contoso.com"],
    "AddressInfo":[{"Address":"1 Suffolk Ln.","City":{"CountryRegion":"United States","Name":"Boise","Region":"ID"}}],
    "Gender":"Female"
}
```

## Update an Entity  

The OData services *SHOULD* support **PATCH** as the preferred means of updating an entity. But also services *MAY* additionally support **PUT**. The request below updates the `Emails` of a person using **PATCH**:

**HTTP PATCH Request**

``` json
PATCH serviceRoot/People('russellwhyte')
    OData-Version: 4.0
    Content-Type: application/json;odata.metadata=minimal
    Accept: application/json
{
    "@odata.type" : "Microsoft.OData.SampleService.Models.TripPin.Person",
    "Emails" :  ["Russell@example.com", "Russell@contoso.com", "newRussell@contoso.com"]
}
```

**Response Payload**

``` json
HTTP/1.1 204 No Content
```

## Remove an Entity

The request below deletes the `Person` with `UserName` *'vincentcalabrese'*:

```
DELETE serviceRoot/People('vincentcalabrese')
```

**Response Payload**

```json
HTTP/1.1 204 No Content
OData-Version: 4.0
```