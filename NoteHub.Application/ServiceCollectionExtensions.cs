using Microsoft.Extensions.DependencyInjection;
using NoteHub.Application.Interfaces;
using NoteHub.Application.Services;

namespace NoteHub.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ITagService, TagService>();
        
        return services;
    }
}