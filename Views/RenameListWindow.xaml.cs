using System.Windows;
using ToDo.Model;

namespace ToDo.Views
{
    /// <summary>
    /// View to rename the selected list.
    /// </summary>
    public partial class RenameListWindow : Window
    {
        public string ListName { get; set; }
        public ToDoList ToDoList { get; set; }
        public RenameListWindow(ToDoList toDoList)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            ToDoList = toDoList;
            ListName = toDoList.Name;
            DataContext = this;
            _ = TextBoxName.Focus();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (ListName is null)
            {
                _ = MessageBox.Show("Der neue Name darf nicht leer sein.");
                return;
            }
            if (ListName is "")
            {
                _ = MessageBox.Show("Der neue Name darf nicht leer sein.");
                return;
            }
            DialogResult = ListName != ToDoList.Name;
        }
    }
}
