using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DIAttrib;

public class DIAttribSetup {

}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DISingletonAttribute : Attribute {
    public Type ServiceType { get; }

    public DISingletonAttribute(Type serviceType) {
        ServiceType = serviceType;
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DIScopedAttribute : Attribute {
    public Type ServiceType { get; }

    public DIScopedAttribute(Type serviceType) {
        ServiceType = serviceType;
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DITransientAttribute : Attribute {
    public Type ServiceType { get; }

    public DITransientAttribute(Type serviceType) {
        ServiceType = serviceType;
    }
}

public static class ServiceCollectionExtensions {
    public static void AddAttributedServices(this IServiceCollection services, params Assembly[] assemblies) {
        foreach (var assembly in assemblies) {
            foreach (var type in assembly.GetTypes()) {
                var singletonAttr = type.GetCustomAttribute<DISingletonAttribute>();
                if (singletonAttr != null) {
                    services.AddSingleton(singletonAttr.ServiceType, type);
                }

                var scopedAttr = type.GetCustomAttribute<DIScopedAttribute>();
                if (scopedAttr != null) {
                    services.AddScoped(scopedAttr.ServiceType, type);
                }

                var transientAttr = type.GetCustomAttribute<DITransientAttribute>();
                if (transientAttr != null) {
                    services.AddTransient(transientAttr.ServiceType, type);
                }
            }
        }
    }
}
