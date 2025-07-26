using NoteHub.Application.Interfaces;
using NoteHub.Domain.Abstractions;
using NoteHub.Domain.Entities;

namespace NoteHub.Application.Services;

public class TagService(ITagRepository tagRepository) : ITagService
{
    public Task<IEnumerable<Tag>> GetAllAsync()
        => tagRepository.GetAllAsync();

    public Task<Tag?> GetByIdAsync(int id)
        => tagRepository.GetByIdAsync(id);

    public Task<int> CreateAsync(Tag tag)
        => tagRepository.CreateAsync(tag);

    public Task<bool> UpdateAsync(Tag tag)
        => tagRepository.UpdateAsync(tag);

    public Task<bool> DeleteAsync(int id)
        => tagRepository.DeleteAsync(id);
}