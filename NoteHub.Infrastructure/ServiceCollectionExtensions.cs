using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteHub.Domain.Abstractions;
using NoteHub.Infrastructure.Persistence;
using NoteHub.Infrastructure.Repositories;

namespace NoteHub.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DatabaseContext>();

        services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}