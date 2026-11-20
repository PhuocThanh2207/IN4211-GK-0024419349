using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    // 1. Lấy danh sách sự kiện của CLB: GET /api/clubs/{clubId}/events
    [HttpGet("api/clubs/{clubId:guid}/events")]
    public IActionResult GetEventsByClub(Guid clubId)
    {
        var events = _eventService.GetByClubId(clubId);
        return Ok(events);
    }

    // 2. Xem một sự kiện theo Id: GET /api/events/{id}
    [HttpGet("api/events/{id:guid}")]
    public IActionResult GetEventById(Guid id)
    {
        var ev = _eventService.GetById(id);
        if (ev == null)
            return NotFound(new { message = "Event not found." });

        return Ok(ev);
    }

    // 3. Tạo sự kiện mới cho CLB: POST /api/clubs/{clubId}/events
    [HttpPost("api/clubs/{clubId:guid}/events")]
    public IActionResult CreateEvent(Guid clubId, [FromBody] CreateEventRequest request)
    {
        try
        {
            var created = _eventService.CreateEvent(clubId, request.Title, request.Date, request.Capacity);
            return CreatedAtAction(nameof(GetEventById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 4. Xoá một sự kiện: DELETE /api/events/{id}
    [HttpDelete("api/events/{id:guid}")]
    public IActionResult DeleteEvent(Guid id)
    {
        var deleted = _eventService.Delete(id);
        if (!deleted)
            return NotFound(new { message = "Event not found." });

        return NoContent();
    }
}

public record CreateEventRequest(string Title, DateTime Date, int Capacity);