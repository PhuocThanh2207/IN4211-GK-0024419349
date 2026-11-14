using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();

    public void Add(Event ev) => _events.Add(ev);

    public IEnumerable<Event> GetByClubId(Guid clubId) =>
        _events.Where(e => e.ClubId == clubId).ToList();

    public Event? GetById(Guid id) =>
        _events.FirstOrDefault(e => e.Id == id);

    public bool Delete(Guid id)
    {
        var ev = GetById(id);
        if (ev == null) return false;
        return _events.Remove(ev);
    }
}