using NoteHub.Domain.Entities;

namespace NoteHub.Application.Interfaces;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note?> GetNoteByIdAsync(Guid id);
    Task AddNoteAsync(Note note);
    Task UpdateNoteAsync(Note note);
    Task DeleteNoteAsync(Guid id);
    Task<IEnumerable<Tag>> GetTagsForNoteAsync(Guid noteId);
    Task AddTagToNoteAsync(Guid noteId, int tagId);
    Task RemoveTagFromNoteAsync(Guid noteId, int tagId);

}