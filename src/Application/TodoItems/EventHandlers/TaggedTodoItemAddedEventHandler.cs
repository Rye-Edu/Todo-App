using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Events;

namespace Todo_App.Application.TodoItems.EventHandlers;
public class TaggedTodoItemAddedEventHandler: INotificationHandler<TaggedTodoItemAddedEvent>
{
    public Task Handle(TaggedTodoItemAddedEvent notification, CancellationToken cancellationToken)
    {
        
            notification.TagItem!.TodoItems!.Add(notification.TodoItem);
            
        return Task.CompletedTask;
    }
}
