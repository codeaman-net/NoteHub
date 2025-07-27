using Microsoft.Extensions.DependencyInjection;
using NoteHub.Application.Factories.Abstract;
using NoteHub.Application.Factories.Concrete;
using NoteHub.Application.Interfaces;
using NoteHub.Application.Services;
using NoteHub.Application.Strategies.Concrete;
using Serilog;

namespace NoteHub.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMemoryCache();
        
        services.AddScoped<ActiveNoteFilterStrategy>();
        services.AddScoped<Last7DaysNoteFilterStrategy>();
        services.AddScoped<INoteFilterStrategyFactory, NoteFilterStrategyFactory>();
        
        services.AddSingleton(Log.Logger);
        
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ITagService, TagService>();
        
        return services;
    }
}