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
using System.Data.SqlTypes;
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

        [ObservableProperty]
        private string? _selectedStudentGrade;

        private ObservableCollection<StudentGrade> _students;
        public ObservableCollection<StudentGrade> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        private readonly IStudentRepository _studentRepository;
        private readonly IDeleteStudentService _deleteStudentService;
        private readonly IEditProductService _editProductService;

        public HomePageViewModel()
        {
            _studentRepository = new StudentRepository();
            _editProductService = new EditProductService();
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
            SelectedStudentGrade = SelectedStudent.Grade + "";
            IsEditModalVisible = true;
        }

        [RelayCommand]
        public void TriggerDeleteModal(int id)
        {
            SelectedStudent = _studentRepository.GetStudent(id);
            IsDeleteModalVisible = true;
        }

        [RelayCommand]
        public void HideModal()
        {
            IsDeleteModalVisible = false;
            IsEditModalVisible = false;
        }

        [RelayCommand]
        public void DeleteStudent(int id)
        {
            _deleteStudentService.DeleteStudent(id);
            StudentGrade temp = Students.First(x => x.Student.Id == id);
            if (temp != null)
            {
                Students.Remove(temp);
            }
            IsDeleteModalVisible=false;
        }

        [RelayCommand]
        public void EditStudent(StudentGrade student)
        {
            _editProductService.UpdateStudent(student.Student.Id, student.Student.FirstName, student.Student.MiddleName, student.Student.LastName, SqlDecimal.Parse(SelectedStudentGrade));
            StudentGrade temp = Students.First(x => x.Student.Id == student.Student.Id);
            if (temp != null)
            {
                student.Grade= SqlDecimal.ConvertToPrecScale(SqlDecimal.Parse(SelectedStudentGrade), 4, 2);
                student.Student.FullName = $"{student.Student.FirstName} {student.Student.MiddleName} {student.Student.LastName}";
                Students[Students.IndexOf(temp)] = student;
            }
            IsEditModalVisible=false;
        }
    }
}