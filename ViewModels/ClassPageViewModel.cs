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

        private ObservableCollection<Block> _blocks;
        public ObservableCollection<Block> Blocks
        {
            get => _blocks;
            set => SetProperty(ref _blocks, value);
        }

        private readonly IStudentRepository _studentRepository;
        private readonly IBlockRepository _blockRepository;

        public ClassPageViewModel()
        {
            _studentRepository = new StudentRepository();
            _blockRepository = new BlockRepository();
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

        private void LoadBlocksWithStudents()
        {
            
        }
    }
    public record BlockWithStudent()
}