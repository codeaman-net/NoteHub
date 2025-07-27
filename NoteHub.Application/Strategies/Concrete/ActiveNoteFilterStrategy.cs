using NoteHub.Application.Strategies.Abstract;
using NoteHub.Domain.Entities;
using NoteHub.Domain.Enums;

namespace NoteHub.Application.Strategies.Concrete;

public class ActiveNoteFilterStrategy : INoteFilterStrategy
{
    public Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes)
    {
        var result = notes.Where(n => n.Status == NoteStatus.Active);
        return Task.FromResult(result);
    }
}