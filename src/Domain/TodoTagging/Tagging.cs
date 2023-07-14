using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Todo_App.Domain.TodoTagging;
public class Tagging
{
    private readonly TagItem? _tagItem;
    private readonly TodoItem? _todoItem;
    private readonly List<TagItem> _tagItems = new List<TagItem>();
    private readonly List<TodoItem> _todoItems = new List<TodoItem>();

    public TagItem Tag { get; private set; }

    public Tagging(TagItem tagName) { 
        Tag = tagName;
    }
    //public Tagging(TagItem tagItem, TodoItem todoItem)
    //{
    //    _tagItem = tagItem;
    //    _todoItem = todoItem;
    //}

    public void CreateTagITem(string TagName)
    {

        if (!string.IsNullOrEmpty(TagName))
        {
             new Tagging(new TagItem(TagName));
            
        }
       // throw new Exception("Invalid TagName");
    }

    public void AddTagOnTodoItem(int TagItemId, int todoItemID)
    {

        var tagId = _tagItems.Find(id => id.Id == TagItemId);

        if (tagId is not null)
        {
            var todoID = _todoItems.SingleOrDefault(id => id.Id == todoItemID);
            if (todoID is not null)
            {
                _tagItems.Add(_tagItem);
            }
        }

    }
}
