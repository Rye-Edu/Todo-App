using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Events;
public class TaggedTodoItemAddedEvent:BaseEvent
{
  

    public TaggedTodoItemAddedEvent(TodoItem todoItem, TagItem tagItem)
    {
        TodoItem = todoItem;
        TagItem = tagItem;
        
    }

    public TodoItem TodoItem { get; }
    public TagItem TagItem { get; }
}
