namespace Accreditation.Application.Abstractions.EventBusService;

using System;
using System.Collections.Generic;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Type>> _handlers;
    private readonly IServiceProvider _serviceProvider;

    public InMemoryEventBus(IServiceProvider serviceProvider)
    {
        _handlers = new Dictionary<Type, List<Type>>();
        _serviceProvider = serviceProvider;
    }

    public void Publish<T>(T @event) where T : IntegrationEvent
    {
        var eventType = @event.GetType();
        if (_handlers.ContainsKey(eventType))
        {
            foreach (var handlerType in _handlers[eventType])
            {
                var handler = _serviceProvider.GetService(handlerType);
                if (handler != null)
                {
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    var method = concreteType.GetMethod("Handle");
                    if (method != null)
                    {
                        method.Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventType = typeof(T);
        var handlerType = typeof(TH);

        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = new List<Type>();
        }

        if (_handlers[eventType].Contains(handlerType))
        {
            throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventType.Name}'", nameof(handlerType));
        }

        _handlers[eventType].Add(handlerType);
    }
}

