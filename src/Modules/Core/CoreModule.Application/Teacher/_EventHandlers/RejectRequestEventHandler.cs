using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Teacher.Events;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.Teacher._EventHandlers;

class RejectRequestEventHandler : INotificationHandler<RejectTeacherRequestEvent>
{
    private readonly IEventBus _eventBus;

    public RejectRequestEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(RejectTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new NewNotificationIntegrationEvent()
        {
            CreationDate = notification.CreationDate,
            Title = "request teacherی شما رد شد",
            Message = $"user گرامی request teacherی شما به دلیل زیر رد شد : <hr/><p>{notification.Description}</p>",
            UserId = notification.UserId
        }, "", Exchanges.NotificationExchange, ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}