# DIAttrib - Dependency Injection via .NET Attributed Programming

DIAttrib is a pre-release implementation of dependency injection via attributed programming.

For example, if you want to introduce a new service by applying an attribute to the service 
class, first set up your services as follows:

```csharp
using DIAttrib;

public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddAttributedServices(Assembly.GetExecutingAssembly());
    }
}
```

Then, when you create new service classes, apply an attribute that defines the appropriate 
lifetime for the service.

```csharp
[DISingleton(typeof(IFooService))]
public class FooService : IFooService {
   public FooService() {}
   public DoStuff() {}
}
```

The `AddAttributedServices` method will take care of the rest.
