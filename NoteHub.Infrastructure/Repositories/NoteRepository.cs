using System.Data;
using Dapper;
using Newtonsoft.Json;
using NoteHub.Domain.Abstractions;
using NoteHub.Domain.Entities;
using NoteHub.Infrastructure.Persistence;

namespace NoteHub.Infrastructure.Repositories;

public class NoteRepository(DatabaseContext dbContext) : BaseRepository(dbContext), INoteRepository
{

    public async Task<Note?> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Notes WHERE Id = @Id;";
        using var connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<NoteDTO>(sql, new { Id = id });
        return result == null ? null : MapFromDTO(result);
    }
    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Notes;";
        using var connection = CreateConnection();
        var result = await connection.QueryAsync<NoteDTO>(sql);
        return result.Select(MapFromDTO);
    }
    public async Task AddAsync(Note note)
    {
        const string sql = "INSERT INTO Notes (Id, Title, Content, CreatedAt, UpdatedAt, Status, Tags) VALUES (@Id, @Title, @Content, @CreatedAt, @UpdatedAt, @Status, @TagsJson);";

        var parameters = new
        {
            note.Id,
            note.Title,
            note.Content,
            note.CreatedAt,
            note.UpdatedAt,
            Status = (int)note.Status,
            TagsJson = JsonConvert.SerializeObject(note.Tags)
        };
        
        using var connection = CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }
    public async Task UpdateAsync(Note note)
    {
        const string sql = @"UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt, Status = @Status, Tags = @TagsJson WHERE Id = @Id;";

        var parameters = new
        {
            note.Id,
            note.Title,
            note.Content,
            note.UpdatedAt,
            Status = (int)note.Status,
            TagsJson = JsonConvert.SerializeObject(note.Tags)
        };
        
        using var connection = CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }
    public async Task DeleteAsync(Guid id)
    {
        const string sql = "DELETE FROM Notes WHERE Id = @Id;";
        
        using var connection = CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }
    public async Task<IEnumerable<Tag>> GetTagsForNoteAsync(Guid noteId)
    {
        using var connection = CreateConnection();
        var sql = @"
        SELECT t.* FROM Tags t
        INNER JOIN NoteTags nt ON t.Id = nt.TagId
        WHERE nt.NoteId = @NoteId";
        return await connection.QueryAsync<Tag>(sql, new { NoteId = noteId });
    }

    public async Task AddTagToNoteAsync(Guid noteId, int tagId)
    {
        using var connection = CreateConnection();
        var sql = @"
        IF NOT EXISTS(SELECT 1 FROM NoteTags WHERE NoteId = @NoteId AND TagId = @TagId)
        INSERT INTO NoteTags (NoteId, TagId) VALUES (@NoteId, @TagId)";
        await connection.ExecuteAsync(sql, new { NoteId = noteId, TagId = tagId });
    }

    public async Task RemoveTagFromNoteAsync(Guid noteId, int tagId)
    {
        using var connection = CreateConnection();
        var sql = "DELETE FROM NoteTags WHERE NoteId = @NoteId AND TagId = @TagId";
        await connection.ExecuteAsync(sql, new { NoteId = noteId, TagId = tagId });
    }

    private class NoteDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
        public string Tags { get; set; } = "[]";
    }
    private Note MapFromDTO(NoteDTO dto) => new Note
    {
        Id = dto.Id,
        Title = dto.Title,
        Content = dto.Content,
        CreatedAt = dto.CreatedAt,
        UpdatedAt = dto.UpdatedAt,
        Status = (Domain.Enums.NoteStatus)dto.Status,
        Tags = JsonConvert.DeserializeObject<List<Tag>>(dto.Tags) ?? []
    };
}