using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities;
public class TagItem:BaseAuditableEntity
{

    public string? TagName { get;  set; }
    public ICollection<TodoItem>? TodoItems { get;  set; }

    public TagItem(){}

    public TagItem(string tagName)
    {
        TagName = tagName;
    }
}
