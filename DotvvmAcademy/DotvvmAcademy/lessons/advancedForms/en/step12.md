The CustomerInfo Class
======================
In the previous steps, we have placed all properties - `FirstName`, `LastName`, `Role` and `SelectedCountryId` in the same class with the `Countries` collection.

That's not the best practise, because we are mixing the information about specific customer with some other data, which is e.g. the list of countries.
It is a good idea to separate properties that represent state of some entity (customer, in this case), into a separate class. This class often contains only properties and no methods.
It is called DTO - Data Transfer Object.

So, we have moved the properties in the `CustomerInfo` class:

```CSHARP
public class CustomerInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public int SelectedCountryId { get; set; }
}
```

The `Countries` collection stays in the viewmodel.