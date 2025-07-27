using Microsoft.Extensions.Caching.Memory;
using NoteHub.Application.Strategies.Abstract;
using NoteHub.Domain.Entities;

namespace NoteHub.Application.Decorators;

public class CachingNoteFilterStrategyDecorator(INoteFilterStrategy inner, IMemoryCache cache) : INoteFilterStrategy
{
    public async Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes)
    {
        var cacheKey = GenerateCacheKey(inner, notes);

        if (cache.TryGetValue(cacheKey, out IEnumerable<Note>? cached))
            return cached!;

        var result = await inner.FilterAsync(notes);

        // Gelişmiş ayarlar
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), 
            SlidingExpiration = TimeSpan.FromMinutes(3)
        };

        cache.Set(cacheKey, result, options);

        return result;
    }

    private string GenerateCacheKey(INoteFilterStrategy strategy, IEnumerable<Note> notes)
    {
        var noteIds = notes.Select(n => n.Id).OrderBy(id => id);
        var hash = string.Join(",", noteIds).GetHashCode();
        return $"{strategy.GetType().Name}_Notes_{hash}";
    }
}