using NoteHub.Domain.Entities;

namespace NoteHub.Application.Interfaces;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetAllAsync();
    Task<Tag?> GetByIdAsync(int id);
    Task<int> CreateAsync(Tag tag);
    Task<bool> UpdateAsync(Tag tag);
    Task<bool> DeleteAsync(int id);
}