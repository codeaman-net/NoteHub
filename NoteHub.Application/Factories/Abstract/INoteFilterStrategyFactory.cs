using NoteHub.Application.Strategies.Abstract;
using NoteHub.Domain.Enums;

namespace NoteHub.Application.Factories.Abstract;

public interface INoteFilterStrategyFactory
{
    INoteFilterStrategy Create(NoteFilterType type, string? keyword = null);
}