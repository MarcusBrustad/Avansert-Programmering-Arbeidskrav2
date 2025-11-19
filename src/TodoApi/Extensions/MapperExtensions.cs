using TodoApi.Mappers;

namespace TodoApi.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        var assembly = typeof(IMapper<,>).Assembly;

        var mapperTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract:false} && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
            .ToList();
        
        
        foreach (var mapperType in mapperTypes)
        {
            var interfaceType = mapperType.GetInterfaces()
                .First(i => i is { IsGenericType: true} && i.GetGenericTypeDefinition() == typeof(IMapper<,>));
            services.AddSingleton(interfaceType, mapperType);
        }

        return services;

    }
    
}