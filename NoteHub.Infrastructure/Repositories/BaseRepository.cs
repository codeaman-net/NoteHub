using System.Data;
using NoteHub.Infrastructure.Persistence;

namespace NoteHub.Infrastructure.Repositories;

public abstract class BaseRepository(DatabaseContext context)
{
    protected IDbConnection CreateConnection() => context.CreateConnection();
}