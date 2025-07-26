using Dapper;
using NoteHub.Domain.Abstractions;
using NoteHub.Domain.Entities;
using NoteHub.Infrastructure.Persistence;

namespace NoteHub.Infrastructure.Repositories;

public class TagRepository(DatabaseContext context): BaseRepository(context), ITagRepository
{
    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Tag>("SELECT * FROM Tags");
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Tag>("SELECT * FROM Tags WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> CreateAsync(Tag tag)
    {
        using var connection = CreateConnection();
        var sql = @"INSERT INTO Tags (Name) VALUES (@Name);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
        return await connection.ExecuteScalarAsync<int>(sql, tag);
    }

    public async Task<bool> UpdateAsync(Tag tag)
    {
        using var connection = CreateConnection();
        var sql = @"UPDATE Tags SET Name = @Name WHERE Id = @Id";
        var rows = await connection.ExecuteAsync(sql, tag);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = CreateConnection();
        var sql = @"DELETE FROM Tags WHERE Id = @Id";
        var rows = await connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }
}