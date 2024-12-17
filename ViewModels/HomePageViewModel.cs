using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using StudentManagementSystem.Helper;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentManagementSystem.Services;
using System.Linq;
namespace StudentManagementSystem.ViewModels
{
    partial class HomePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private int _negative = -1;

        [ObservableProperty]
        private bool _isEditModalVisible = false;
        [ObservableProperty]
        private bool _isDeleteModalVisible = false;

        [ObservableProperty]
        private StudentGrade _selectedStudent;

        private ObservableCollection<StudentGrade> _students;
        public ObservableCollection<StudentGrade> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        private readonly IStudentRepository _studentRepository;
        private readonly IDeleteStudentService _deleteStudentService;

        public HomePageViewModel()
        {
            _studentRepository = new StudentRepository();
            _deleteStudentService = new DeleteStudentService();
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
        public void TriggerEditModal(int id)
        {

                SelectedStudent = _studentRepository.GetStudent(id);
            IsEditModalVisible = !IsEditModalVisible;
        }

        [RelayCommand]
        public void TriggerDeleteModal(int id)
        {
            Debug.WriteLine(id);
                SelectedStudent = _studentRepository.GetStudent(id);
            IsDeleteModalVisible = !IsDeleteModalVisible;
        }

        [RelayCommand]
        public void DeleteStudent(int id)
        {
            Debug.WriteLine(id);
            _deleteStudentService.DeleteStudent(id);
            StudentGrade temp = Students.First(x => x.Student.Id == id);
            if (temp != null)
            {
                Debug.WriteLine("readceasda");
                Students.Remove(temp);
            }
            IsDeleteModalVisible = false;
        }
    }
}