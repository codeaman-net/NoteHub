using NoteHub.Application.Interfaces;
using NoteHub.Domain.Abstractions;
using NoteHub.Domain.Entities;

namespace NoteHub.Application.Services;

public class NoteService(INoteRepository noteRepository) : INoteService
{
    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await noteRepository.GetAllAsync();
    }

    public async Task<Note?> GetNoteByIdAsync(Guid id)
    {
        return await noteRepository.GetByIdAsync(id);
    }

    public async Task AddNoteAsync(Note note)
    {
        note.Id = Guid.NewGuid();
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = null;

        await noteRepository.AddAsync(note);
    }

    public async Task UpdateNoteAsync(Note note)
    {
        note.UpdatedAt = DateTime.UtcNow;
        await noteRepository.UpdateAsync(note);
    }

    public async Task DeleteNoteAsync(Guid id)
    {
        await noteRepository.DeleteAsync(id);
    }
    public Task<IEnumerable<Tag>> GetTagsForNoteAsync(Guid noteId)
        => noteRepository.GetTagsForNoteAsync(noteId);

    public Task AddTagToNoteAsync(Guid noteId, int tagId)
        => noteRepository.AddTagToNoteAsync(noteId, tagId);

    public Task RemoveTagFromNoteAsync(Guid noteId, int tagId)
        => noteRepository.RemoveTagFromNoteAsync(noteId, tagId);

}