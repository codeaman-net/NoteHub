namespace NoteHub.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<Note> Notes { get; set; } = [];
}