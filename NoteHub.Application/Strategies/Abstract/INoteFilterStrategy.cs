using NoteHub.Domain.Entities;

namespace NoteHub.Application.Strategies.Abstract;

public interface INoteFilterStrategy
{
    Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes);
}