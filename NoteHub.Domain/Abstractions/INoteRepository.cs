using NoteHub.Domain.Entities;

namespace NoteHub.Domain.Abstractions;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(Guid id);
    Task<IEnumerable<Note>> GetAllAsync();
    Task AddAsync(Note note);
    Task UpdateAsync(Note note);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Tag>> GetTagsForNoteAsync(Guid noteId);
    Task AddTagToNoteAsync(Guid noteId, int tagId);
    Task RemoveTagFromNoteAsync(Guid noteId, int tagId);
}