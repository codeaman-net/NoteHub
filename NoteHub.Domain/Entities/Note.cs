using NoteHub.Domain.Enums;

namespace NoteHub.Domain.Entities;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public NoteStatus Status { get; set; } = NoteStatus.Active;
    
    public List<Tag> Tags { get; set; } = [];
}