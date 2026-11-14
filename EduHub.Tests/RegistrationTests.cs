using EduHub.Domain.Entities;

namespace EduHub.Tests;

public class RegistrationTests
{
    [Fact]
    public void Confirm_AfterCancel_ShouldThrowInvalidOperationException_AndRemainCancelled()
    {
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        var r = Registration.Create(memberId, eventId);
        r.Cancel();

        Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Create_WithValidIds_ShouldInitializeWithPendingStatus()
    {
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        var r = Registration.Create(memberId, eventId);

        Assert.Equal(RegistrationStatus.Pending, r.Status);
        Assert.Equal(memberId, r.MemberId);
        Assert.Equal(eventId, r.EventId);
    }

    [Fact]
    public void Confirm_WhenPending_ShouldTransitionToConfirmed()
    {
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());

        r.Confirm();

        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Create_WithEmptyGuid_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, Guid.NewGuid()));
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.NewGuid(), Guid.Empty));
    }
}
