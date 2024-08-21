// ReSharper disable UnusedMember.Local

// ReSharper disable ArrangeTypeMemberModifiers

namespace WebAPI;

using Controllers;
using Pure.DI;
using Pure.DI.MS;
using WeatherForecast;
using static Pure.DI.Lifetime;

internal partial class Composition : ServiceProviderFactory<Composition>
{
    static void Setup() => DI.Setup()
        .DependsOn(Base)
        // Specifies not to attempt to resolve types whose fully qualified name
        // begins with Microsoft.Extensions., Microsoft.AspNetCore.
        // since ServiceProvider will be used to retrieve them.
        .Hint(
            Hint.OnCannotResolveContractTypeNameRegularExpression,
            @"^Microsoft\.(Extensions|AspNetCore)\..+$")
        // Scoped lifetime means that I expected to have a new CounterService per Http request
        .Bind().As(Scoped).To<CounterService>()
        .Root<CounterController>();
}