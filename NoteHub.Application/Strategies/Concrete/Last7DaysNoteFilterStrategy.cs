using NoteHub.Application.Strategies.Abstract;
using NoteHub.Domain.Entities;

namespace NoteHub.Application.Strategies.Concrete;

public class Last7DaysNoteFilterStrategy : INoteFilterStrategy
{
    public Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes)
    {
        var result = notes.Where(n => n.CreatedAt >= DateTime.UtcNow.AddDays(-7));
        return Task.FromResult(result);
    }
}