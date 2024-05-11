namespace StarWarsTracker.Domain.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsGenericArguments(this Type type, Type genericTypeDefinition) =>
            type.GetInterfaces().Any(_ => _.IsGenericType && _.GetGenericTypeDefinition() == genericTypeDefinition);
    }
}
