using TodoApi.Mappers;

namespace TodoApi.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection RegisterMappers(this IServiceCollection services)
    {
        var assembly = typeof(IMapper<,>).Assembly;

        var mapperTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
            .ToList();
        
        
        foreach (var mapperType in mapperTypes)
        {
            var interfaceType = mapperType.GetInterfaces().First(i => 
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>));
            services.AddSingleton(interfaceType, mapperType);
        }

        return services;

    }
}