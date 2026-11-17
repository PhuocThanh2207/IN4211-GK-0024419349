using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;

namespace EduHub.Tests;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_WithValidInput_ShouldCallAddAndSendOnce()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        var mockNotification = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotification.Object);

        // Act
        service.CreateEvent(Guid.NewGuid(), "Workshop OOP", DateTime.UtcNow.AddDays(3), 100);

        // Assert
        mockRepo.Verify(r => r.Add(It.IsAny<Event>()), Times.Once);
        mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
    }
}