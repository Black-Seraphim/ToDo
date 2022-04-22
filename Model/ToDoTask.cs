using System;

namespace ToDo.Model
{
    public class ToDoTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime LastChanged { get; set; }
        public bool Finished { get; set; }
    }
}
