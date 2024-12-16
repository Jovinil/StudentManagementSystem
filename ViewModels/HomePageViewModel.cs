using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using StudentManagementSystem.Helper;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace StudentManagementSystem.ViewModels
{
    partial class HomePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isEditModalVisible = false;
        [ObservableProperty]
        private bool _isDeleteModalVisible = false;

        private ObservableCollection<StudentGrade> _students;
        public ObservableCollection<StudentGrade> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        private readonly IStudentRepository _studentRepository;

        public HomePageViewModel()
        {
            _studentRepository = new StudentRepository();
            LoadStudents();
        }
        private async void LoadStudents()
        {
            _students = new ObservableCollection<StudentGrade>();
            await foreach(var student in _studentRepository.GetAllWithGrade())
            {
                if(student != null)
                {
                    
                    Students.Add(student);
                }
            }
        }

        [RelayCommand]
        public void TriggerEditModal()
        { 
            IsEditModalVisible = !IsEditModalVisible;
        }

        [RelayCommand]
        public void TriggerDeleteModal()
        {
            IsDeleteModalVisible = !IsDeleteModalVisible;
        }
    }
}