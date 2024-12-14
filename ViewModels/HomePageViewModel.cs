using System.Collections.ObjectModel;
using System;
namespace StudentManagementSystem.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<Student> StudentData { get; set; }

        public HomePageViewModel()
        {
            // Hardcoded student data
            StudentData = new ObservableCollection<Student>
            {
                new Student { Name = "John Doe", Block = "A", Year = "2023", Grade = "A+" },
                new Student { Name = "Jane Smith", Block = "B", Year = "2022", Grade = "B" },
                new Student { Name = "Michael Johnson", Block = "C", Year = "2021", Grade = "C" }
            };
            
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Block { get; set; }
        public string Year { get; set; }
        public string Grade { get; set; }
    }
}