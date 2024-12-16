using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using StudentManagementSystem.Views;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.ViewModels
{
    partial class ClassPageViewModel : ViewModelBase
    {
        private ObservableCollection<StudentGrade> _students;
        public ObservableCollection<StudentGrade> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        private readonly IStudentRepository _studentRepository;

        public ClassPageViewModel()
        {
            _studentRepository = new StudentRepository();
            LoadStudents();
        }

        private async void LoadStudents()
        {
            _students = new ObservableCollection<StudentGrade>();
            await foreach (var student in _studentRepository.GetAllWithGrade())
            {
                if (student != null)
                {

                    Students.Add(student);
                }
            }
        }
    }
}