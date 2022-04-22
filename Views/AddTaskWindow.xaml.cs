using System;
using System.Windows;
using ToDo.Model;

namespace ToDo.Views
{
    /// <summary>
    /// View to add a task to the selected TaskList
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public ToDoTask ToDoTask { get; set; }
        public string TaskName { get; set; }
        public string TaskNote { get; set; }
        public string ListName { get; set; }

        public AddTaskWindow(string listName)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            ListName = listName;
            Title = $"Aufgabe zu \"{listName}\" hinzufügen.";
            DataContext = this;
            TextBoxName.Focus();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskName is null)
            {
                _ = MessageBox.Show("Bitte gib einen Namen für die Aufgabe ein.");
                return;
            }
            if (TaskName is "")
            {
                _ = MessageBox.Show("Bitte gib einen Namen für die Aufgabe ein.");
                return;
            }
            if (TaskNote is null)
            {
                TaskNote = "";
            }
            ToDoTask = new()
            { 
                Name = TaskName,
                Note = TaskNote,
                LastChanged = DateTime.Now,
                Finished = false
            };
            DialogResult = true;
        }
    }
}
