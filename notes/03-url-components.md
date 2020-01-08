# URL Components

URLs represent individual resources, collections of resources, or operations, and clients interact with those resources using standard GET, PUT, PATCH, POST, and DELETE operations. An OData service URL consists of 3 parts:

```
http://host:port/path/SampleService.svc/Categories(1)/Products?$top=2&$orderby=Name
```

<dl>
    <dt>
        <strong>Service Root URL</strong> - <code>http://host:port/path/SampleService.svc</code>
    </dt>
    <dd>
        The base URL of the service. When a *GET* request is made to this url it will return a service document that defines all the resources available via that service.
    </dd>
    <dt>
        <strong>Resource Path</strong> - <code>/Categories(1)/Products</code>
    </dt>
    <dd>
        A resource by REST definition is an object which is accessible over HTTP by using the standard GET, POST, PUT, PATCH, and DELETE methods. It could be a single object or a collection of similar objects, presented ordered or unordered. An object is constructed of a certain type, has relationships with other objects, and has methods that operate on it. Some examples for resources might be: customers, a single customer, orders related toa  single customer, and so forth. Examples of addressable aspects of these resources as exposed by the data model might be: collections of entities, a single entity, properties, links, operations, and so on.<br /><br />
        Here are a few examples of how a resource could be accessed:
        <ul>
            <li><code>http://host/service/Products</code> - gets access to an entity set</li>
            <li><code>http://host/service/ProductsByCategoryId(catId=2)</code> - allows execution of a function</li>
        </ul>
    </dd>
    <dt>
        <strong>Query Options</strong> - <code>?$top=2&$orderby=Name</code>
    </dt>
    <dd>
        Query options are essentially standardized querystring parameters that can be passed to an OData service to run queries on a requested resource. They can help perform certain operations like <code>select</code>, <code>filter</code>, <code>count</code>, <code>skip</code>, <code>order</code>, <code>search</code>, and <code>format</code> on resources. All OData query options are prefixed by a <code>$</code> sign and they are case-insensitive.<br /><br />
        For e.g. the following url selects a product by color: <code>https://server/products?$filter=color eq 'red'</code>
    </dd>
</dl>