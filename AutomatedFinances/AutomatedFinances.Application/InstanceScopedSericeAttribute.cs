namespace AutomatedFinances.Application;

/// <summary>
/// Tag a service implementation for registration as an instance scoped service
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
internal class InstanceScopedServiceAttribute : Attribute
{
}
