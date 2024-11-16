﻿namespace Accreditation.Application.Abstractions.EventBusService;
public abstract class IntegrationEvent
{
    public DateTime CreationDate { get; private set; }

    protected IntegrationEvent()
    {
        CreationDate = DateTime.UtcNow;
    }
}


