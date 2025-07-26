using Microsoft.AspNetCore.Mvc;
using NoteHub.Application.Interfaces;
using NoteHub.Domain.Entities;

namespace NoteHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController(ITagService tagService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tags = await tagService.GetAllAsync();
        return Ok(tags);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tag = await tagService.GetByIdAsync(id);
        if (tag == null)
            return NotFound();

        return Ok(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tag tag)
    {
        var newId = await tagService.CreateAsync(tag);
        return CreatedAtAction(nameof(GetById), new { id = newId }, tag);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Tag tag)
    {
        var existing = await tagService.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        tag.Id = id;
        var updated = await tagService.UpdateAsync(tag);
        if (!updated)
            return StatusCode(500, "Güncelleme başarısız oldu.");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await tagService.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        var deleted = await tagService.DeleteAsync(id);
        if (!deleted)
            return StatusCode(500, "Silme işlemi başarısız oldu.");

        return NoContent();
    }
}