using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

public interface IEventRepository
{
    void Add(Event ev);
    IEnumerable<Event> GetByClubId(Guid clubId);
    Event? GetById(Guid id);
    bool Delete(Guid id);
}