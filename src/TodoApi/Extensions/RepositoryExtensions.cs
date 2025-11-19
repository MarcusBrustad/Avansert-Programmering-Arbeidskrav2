using TodoApi.Repositories;

namespace TodoApi.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = typeof(IBaseRepository<>).Assembly;
        
        var repositoryTypes = assembly.GetTypes()
            .Where(t => t is {IsClass: true, IsAbstract: false, IsGenericTypeDefinition:false} && t.GetInterfaces()
                .Any(i => i is {IsGenericType: true} && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>)))
            .ToList();

        foreach (var repositoryType in repositoryTypes)
        {
            var interfaceType = repositoryType
                .GetInterfaces()
                .First(i => i is { IsGenericType: false });
            services.AddScoped(interfaceType, repositoryType);
        }
        
                
        return services;
    }
}