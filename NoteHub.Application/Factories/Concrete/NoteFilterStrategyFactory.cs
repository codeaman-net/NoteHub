using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NoteHub.Application.Decorators;
using NoteHub.Application.Factories.Abstract;
using NoteHub.Application.Strategies.Abstract;
using NoteHub.Application.Strategies.Concrete;
using NoteHub.Application.Strategies.Options;
using NoteHub.Domain.Enums;
using Serilog;

namespace NoteHub.Application.Factories.Concrete;

public class NoteFilterStrategyFactory(IServiceProvider provider) : INoteFilterStrategyFactory
{
    public INoteFilterStrategy Create(NoteFilterType type, string? keyword = null)
    {
        var strategy = type switch
        {
            NoteFilterType.Active => provider.GetRequiredService<ActiveNoteFilterStrategy>(),
            NoteFilterType.Last7Days => provider.GetRequiredService<Last7DaysNoteFilterStrategy>(),
            NoteFilterType.Keyword when !string.IsNullOrWhiteSpace(keyword)
                => CreateKeywordStrategy(keyword),
            _ => throw new ArgumentException("Geçersiz filtre türü.")
        };

        var logger = provider.GetRequiredService<ILogger>();
        var cache = provider.GetRequiredService<IMemoryCache>();
        
        strategy = new LoggingNoteFilterStrategyDecorator(strategy, logger);
        strategy = new CachingNoteFilterStrategyDecorator(strategy, cache);

        return strategy;
    }

    private INoteFilterStrategy CreateKeywordStrategy(string keyword)
    {
        var options = new KeywordNoteFilterStrategyOptions { Keyword = keyword };
        return new KeywordNoteFilterStrategy(options);
    }
}