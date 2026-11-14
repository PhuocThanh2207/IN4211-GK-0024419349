using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly INotificationService _notificationService;

    public EventService(IEventRepository eventRepository, INotificationService notificationService)
    {
        _eventRepository = eventRepository;
        _notificationService = notificationService;
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity)
    {
        var ev = Event.Create(clubId, title, date, capacity);
        _eventRepository.Add(ev);
        _notificationService.Send($"Sự kiện mới: {ev.Title}");
        return ev;
    }

    public IEnumerable<Event> GetByClubId(Guid clubId) => _eventRepository.GetByClubId(clubId);

    public Event? GetById(Guid id) => _eventRepository.GetById(id);

    public bool Delete(Guid id) => _eventRepository.Delete(id);
}