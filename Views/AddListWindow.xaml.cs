using System.Windows;
using ToDo.Model;

namespace ToDo.Views
{
    /// <summary>
    /// View to add a Tasklist.
    /// </summary>
    public partial class AddListWindow : Window
    {
        public string ListName { get; set; }
        public ToDoList ToDoList { get; set; }

        public AddListWindow()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            DataContext = this;
            TextBoxName.Focus();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddList_Click(object sender, RoutedEventArgs e)
        {
            if (ListName is null)
            {
                _ = MessageBox.Show("Bitte gib einen Namen für die neue Liste ein.");
                return;
            }
            if (ListName is "")
            {
                _ = MessageBox.Show("Bitte gib einen Namen für die neue Liste ein.");
                return;
            }
            ToDoList = new()
            {
                Name = ListName,
                ToDoTasks = new()
            };
            DialogResult = true;
        }
    }
}
