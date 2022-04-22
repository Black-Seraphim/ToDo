using System;
using System.Collections.Generic;

namespace ToDo.Model
{
    public class ToDoList
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<ToDoTask> ToDoTasks { get; set; }
    }
}
