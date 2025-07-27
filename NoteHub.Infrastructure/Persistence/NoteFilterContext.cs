using NoteHub.Application.Strategies.Abstract;
using NoteHub.Domain.Entities;

namespace NoteHub.Infrastructure.Persistence;

public class NoteFilterContext
{
    private INoteFilterStrategy _strategy;

    public void SetStrategy(INoteFilterStrategy strategy)
    {
        _strategy = strategy;
    }

    public Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes)
    {
        if (_strategy is null)
            throw new InvalidOperationException("Filtreleme stratejisi belirlenmedi.");

        return _strategy.FilterAsync(notes);
    }
}