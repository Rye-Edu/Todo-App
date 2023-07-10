using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities.Aggregate;
public class Todo
{
    private readonly TodoList _todoList;

    public Todo(TodoList todoList)
    {
        _todoList = todoList;
    }

    public void AddTodo(string title, string color) {

        if (!color.Equals("white")) {
            _todoList.Colour = (Colour)color.ToString();
            _todoList.Title = title;
        }
        _todoList.Title = title;
    }

    public void AddTodoItem() { 
    
    }
}
