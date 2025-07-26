using NoteHub.Domain.Entities;

namespace NoteHub.Domain.Abstractions;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync();
    Task<Tag?> GetByIdAsync(int id);
    Task<int> CreateAsync(Tag tag);
    Task<bool> UpdateAsync(Tag tag);
    Task<bool> DeleteAsync(int id);
}