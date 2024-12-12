using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace StudentManagementSystem.ViewModels
{
    public partial class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<Student> StudentData { get; set; }

        public HomePageViewModel()
        {
            // Sample data for the DataGrid
            StudentData = new ObservableCollection<Student>
            {
                new Student { Name = "John Doe", Block = "A", Year = 3, Grade = "A" },
                new Student { Name = "Jane Smith", Block = "B", Year = 2, Grade = "B+" },
                new Student { Name = "Alice Johnson", Block = "C", Year = 4, Grade = "A-" },
                new Student { Name = "Bob Brown", Block = "A", Year = 1, Grade = "C" }
            };
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Block { get; set; }
        public int Year { get; set; }
        public string Grade { get; set; }
    }
}