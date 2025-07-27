using NoteHub.Application.Strategies.Abstract;
using NoteHub.Application.Strategies.Options;
using NoteHub.Domain.Entities;

namespace NoteHub.Application.Strategies.Concrete;

public class KeywordNoteFilterStrategy(KeywordNoteFilterStrategyOptions options) : INoteFilterStrategy
{
    public Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes)
    {
        var keyword = options.Keyword.ToLowerInvariant();

        var result = notes.Where(n =>
            n.Title.ToLower().Contains(keyword) ||
            n.Content.ToLower().Contains(keyword));

        return Task.FromResult(result);
    }
}