using System.Diagnostics;
using NoteHub.Application.Strategies.Abstract;
using NoteHub.Domain.Entities;
using Serilog;

namespace NoteHub.Application.Decorators;

public class LoggingNoteFilterStrategyDecorator(INoteFilterStrategy inner, ILogger logger) : INoteFilterStrategy
{
    public async Task<IEnumerable<Note>> FilterAsync(IEnumerable<Note> notes)
    {
        var strategyName = inner.GetType().Name;

        var sw = Stopwatch.StartNew();
        logger.Information("Filtreleme başladı: {Strategy}", strategyName);

        var result = await inner.FilterAsync(notes);

        sw.Stop();
        logger.Information("Filtreleme tamamlandı: {Strategy}, Süre: {Elapsed} ms, Sonuç: {Count}",
            strategyName, sw.ElapsedMilliseconds, result.Count());

        return result;
    }
}