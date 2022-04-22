using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ToDo.Database;
using ToDo.Model;
using System.Linq;
using System.Windows.Media;
using System.ComponentModel;

namespace ToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BaseModel Model { get; set; }
        public List<ToDoList> ToDoLists { get; set; }
        public List<ToDoTask> ToDoTasks { get; set; }
        public ToDoTask ToDoTask { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Model = Json.Load();
            if (Model is null)
            {
                MessageBoxResult createDatabaseQuestionResult = MessageBox.Show("Datenbank konnte nicht gelesen werden, soll eine neue erstellt werden? " +
                    "Alte Daten werden dabei unter Umständen überschrieben.", "Neue Datenbank erstellen?", MessageBoxButton.YesNo);

                if (createDatabaseQuestionResult == MessageBoxResult.Yes)
                {
                    Json.CreateNew();
                    Model = Json.Load();
                }
                else
                {
                    _ = MessageBox.Show("Konnte keine Daten lesen, bitte überprüfe den Zustand der Datenbank. Anwendung wird beendet.", "Datenbank nicht vorhanden");
                    Close();
                }
            }

            RefreshToDoLists();
            SetContentVisability();
        }

        private void AddList_Click(object sender, RoutedEventArgs e)
        {
            AddListWindow addListWindow = new();

            if (addListWindow.ShowDialog() is not true)
            {
                return;
            }

            if (Model.ToDoLists is null)
            {
                Model.ToDoLists = new();
            }

            Model.ToDoLists.Add(addListWindow.ToDoList);
            RefreshToDoLists();
            ListBoxLists.SelectedIndex = ListBoxLists.Items.Count - 1;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem listBoxItem = (ListBoxItem)ListBoxLists.SelectedItem;
            ToDoList toDoList = Model.ToDoLists.FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxItem.Uid));

            AddTaskWindow addTaskWindow = new(toDoList.Name);
            if (addTaskWindow.ShowDialog() is true)
            {
                toDoList.ToDoTasks.Add(addTaskWindow.ToDoTask);
            }
            RefreshTasks();
            ListBoxTasks.SelectedIndex = ListBoxTasks.Items.Count - 1;
        }

        private void RemoveList_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxLists.SelectedIndex < 0)
            {
                return;
            }

            int newSelectedIndex = ListBoxLists.SelectedIndex - 1;
            if (newSelectedIndex < 0 && ListBoxLists.Items.Count > 0)
            {
                newSelectedIndex = 0;
            }

            ListBoxItem listBoxItem = (ListBoxItem)ListBoxLists.SelectedItem;
            ToDoList toDoList = Model.ToDoLists
                .FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxItem.Uid));

            bool listHasUnfinishedTasks = toDoList.ToDoTasks.Where(toDoTask => !toDoTask.Finished).Any();

            if (listHasUnfinishedTasks)
            {
                MessageBoxResult contentShouldBeDeleted = MessageBox.Show("Die Liste hat noch unerledigte Aufgaben, soll sie wirklich gelöscht werden?", "Liste wirklich löschen?", MessageBoxButton.YesNo);
                if (contentShouldBeDeleted != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            _ = Model.ToDoLists.Remove(toDoList);
            RefreshToDoLists();

            ListBoxLists.SelectedIndex = newSelectedIndex;
        }

        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxLists.SelectedIndex < 0)
            {
                return;
            }
            if (ListBoxTasks.SelectedIndex < 0)
            {
                return;
            }

            int newSelectedIndex = ListBoxTasks.SelectedIndex - 1;
            if (newSelectedIndex < 0 && ListBoxTasks.Items.Count > 0)
            {
                newSelectedIndex = 0;
            }

            ListBoxItem listBoxListItem = (ListBoxItem)ListBoxLists.SelectedItem;
            ListBoxItem listBoxTaskItem = (ListBoxItem)ListBoxTasks.SelectedItem;

            ToDoTask toDoTask = Model.ToDoLists
               .FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxListItem.Uid)).ToDoTasks
               .FirstOrDefault(toDoTasks => toDoTasks.Id == Guid.Parse(listBoxTaskItem.Uid));

            _ = Model.ToDoLists
               .FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxListItem.Uid)).ToDoTasks
               .Remove(toDoTask);

            RefreshToDoLists();
            ListBoxTasks.SelectedIndex = newSelectedIndex;
        }

        private void EditList_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxLists.SelectedIndex < 0)
            {
                return;
            }

            int selectedIndex = ListBoxLists.SelectedIndex;

            ListBoxItem listBoxItem = (ListBoxItem)ListBoxLists.SelectedItem;
            ToDoList toDoList = Model.ToDoLists
                .FirstOrDefault(toDoList => toDoList.Id == Guid.Parse(listBoxItem.Uid));

            RenameListWindow renameListWindow = new(toDoList);
            if (renameListWindow.ShowDialog() is true)
            {
                toDoList.Name = renameListWindow.ListName;
            }
            RefreshToDoLists();
            ListBoxLists.SelectedIndex = selectedIndex;
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTasks.SelectedIndex < 0)
            {
                return;
            }

            if (TextBoxTaskName.Text is "")
            {
                _ = MessageBox.Show("Bitte gib einen Namen für den Task ein.");
                return;
            }
            int selectedTaskIndex = ListBoxTasks.SelectedIndex;

            ListBoxItem listBoxListItem = (ListBoxItem)ListBoxLists.SelectedItem;
            ListBoxItem listBoxTaskItem = (ListBoxItem)ListBoxTasks.SelectedItem;

            ToDoTask toDoTask = Model.ToDoLists
               .FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxListItem.Uid)).ToDoTasks
               .FirstOrDefault(toDoTasks => toDoTasks.Id == Guid.Parse(listBoxTaskItem.Uid));

            bool statusHasChanged = toDoTask.Finished != CheckBoxTaskFinished.IsChecked;

            toDoTask.Name = TextBoxTaskName.Text;
            toDoTask.Note = TextBoxTaskNote.Text;
            toDoTask.Finished = CheckBoxTaskFinished.IsChecked == true;
            toDoTask.LastChanged = DateTime.Now;

            RefreshTasks();
            if (!statusHasChanged)
            {
                ListBoxTasks.SelectedIndex = selectedTaskIndex;
            }
            if (statusHasChanged && CheckBoxShowFinishedTasks.IsChecked is true)
            {
                ListBoxTasks.SelectedIndex = selectedTaskIndex;
            }
        }

        private void RefreshToDoLists()
        {
            if (Model.ToDoLists is null)
            {
                ListBoxLists.ItemsSource = null;
                return;
            }
            List<ListBoxItem> listBoxItems = Model.ToDoLists
                .Select(toDoList => new ListBoxItem()
                {
                    Content = toDoList.Name,
                    Uid = toDoList.Id.ToString()
                })
                .ToList();
            ListBoxLists.ItemsSource = listBoxItems;
            ListBoxLists.Items.Refresh();
        }

        private void RefreshTasks()
        {
            int selectedListIndex = ListBoxLists.SelectedIndex;
            int selectedTaskIndex = ListBoxTasks.SelectedIndex;
            if (selectedListIndex < 0)
            {
                ListBoxTasks.ItemsSource = null;
                return;
            }

            ListBoxItem listBoxItem = (ListBoxItem)ListBoxLists.SelectedItem;

            List<ToDoTask> toDoTasks = Model.ToDoLists
                .FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxItem.Uid)).ToDoTasks
                .ToList();

            if (CheckBoxShowFinishedTasks.IsChecked is not true)
            {
                toDoTasks = toDoTasks.Where(toDoTask => !toDoTask.Finished).ToList();
            }

            List<ListBoxItem> listBoxItems = toDoTasks
                .Select(toDoTask => new ListBoxItem()
                {
                    Content = toDoTask.Name,
                    Uid = toDoTask.Id.ToString(),
                    FontStyle = toDoTask.Finished ? FontStyles.Italic : FontStyles.Normal,
                    Foreground = toDoTask.Finished ? Brushes.Gray : Brushes.Black
                })
                .ToList();

            ListBoxTasks.ItemsSource = listBoxItems;
            ListBoxTasks.Items.Refresh();
            ListBoxTasks.SelectedIndex = selectedTaskIndex;
        }

        private void RefreshTask()
        {
            int selectedTask = ListBoxTasks.SelectedIndex;

            if (selectedTask < 0)
            {
                TextBoxTaskName.Text = "";
                TextBoxTaskNote.Text = "";
                CheckBoxTaskFinished.IsChecked = false;
                LabelTaskLastChanged.Content = "";
                return;
            }

            ListBoxItem listBoxListItem = (ListBoxItem)ListBoxLists.SelectedItem;
            ListBoxItem listBoxTaskItem = (ListBoxItem)ListBoxTasks.SelectedItem;

            ToDoTask toDoTask = Model.ToDoLists
               .FirstOrDefault(toDoLists => toDoLists.Id == Guid.Parse(listBoxListItem.Uid)).ToDoTasks
               .FirstOrDefault(toDoTasks => toDoTasks.Id == Guid.Parse(listBoxTaskItem.Uid));

            TextBoxTaskName.Text = toDoTask.Name;
            TextBoxTaskNote.Text = toDoTask.Note;
            CheckBoxTaskFinished.IsChecked = toDoTask.Finished;
            LabelTaskLastChanged.Content = toDoTask.LastChanged;
        }

        private void SetContentVisability()
        {
            TextBoxTaskName.IsEnabled = ListBoxTasks.SelectedIndex > -1;
            TextBoxTaskNote.IsEnabled = ListBoxTasks.SelectedIndex > -1;
            CheckBoxTaskFinished.IsEnabled = ListBoxTasks.SelectedIndex > -1;

            ButtonRemoveList.IsEnabled = ListBoxLists.SelectedIndex > -1;
            ButtonEditList.IsEnabled = ListBoxLists.SelectedIndex > -1;

            ButtonRemoveTask.IsEnabled = ListBoxTasks.SelectedIndex > -1;
            ButtonEditTask.IsEnabled = ListBoxTasks.SelectedIndex > -1;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Json.Save(Model);
        }

        private void ListBoxLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTasks();
            SetContentVisability();
        }

        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTask();
            SetContentVisability();
        }

        private void ShowFinishedTasks_Checked(object sender, RoutedEventArgs e)
        {
            RefreshTasks();
        }

        private void ShowFinishedTasks_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshTasks();
        }
    }
}
