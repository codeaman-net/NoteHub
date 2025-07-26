using Microsoft.AspNetCore.Mvc;
using NoteHub.Application.Interfaces;
using NoteHub.Domain.Entities;

namespace NoteHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController(INoteService noteService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await noteService.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var note = await noteService.GetNoteByIdAsync(id);
        if (note == null)
            return NotFound();

        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Note note)
    {
        await noteService.AddNoteAsync(note);
        return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Note note)
    {
        var existing = await noteService.GetNoteByIdAsync(id);
        if (existing == null)
            return NotFound();

        note.Id = id;
        await noteService.UpdateNoteAsync(note);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await noteService.GetNoteByIdAsync(id);
        if (existing == null)
            return NotFound();

        await noteService.DeleteNoteAsync(id);
        return NoContent();
    }
    
    [HttpGet("{noteId:guid}/tags")]
    public async Task<IActionResult> GetTagsForNote(Guid noteId)
    {
        var tags = await noteService.GetTagsForNoteAsync(noteId);
        return Ok(tags);
    }

    [HttpPost("{noteId:guid}/tags/{tagId:int}")]
    public async Task<IActionResult> AddTagToNote(Guid noteId, int tagId)
    {
        await noteService.AddTagToNoteAsync(noteId, tagId);
        return NoContent();
    }

    [HttpDelete("{noteId:guid}/tags/{tagId:int}")]
    public async Task<IActionResult> RemoveTagFromNote(Guid noteId, int tagId)
    {
        await noteService.RemoveTagFromNoteAsync(noteId, tagId);
        return NoContent();
    }

}