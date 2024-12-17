using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using StudentManagementSystem.Views;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;
using System.Diagnostics;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.ViewModels
{
    partial class ClassPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public bool _isAddModalVisible = false;

        [ObservableProperty]
        public int _selectedBlock;

        [ObservableProperty]
        public int _selectedCourse;

        private ObservableCollection<Block> _blocks;
        public ObservableCollection<Block> Blocks
        {
            get => _blocks;
            set => SetProperty(ref _blocks, value);
        }

        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get => _courses;
            set => SetProperty(ref _courses, value);
        }

        private ObservableCollection<BlockWithStudent> _blocksWithStudents;
        public ObservableCollection<BlockWithStudent> BlocksWithStudents
        {
            get => _blocksWithStudents;
            set => SetProperty(ref _blocksWithStudents, value);
        }

        private readonly IStudentRepository _studentRepository;
        private readonly IBlockRepository _blockRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAddStudentsService _addStudentsService;

        public ClassPageViewModel()
        {
            _studentRepository = new StudentRepository();
            _courseRepository = new CourseRepository();
            _blockRepository = new BlockRepository();
            _addStudentsService = new AddStudentService();
            LoadBlocks();
            LoadCourses();
            LoadBlocksWithStudents();
        }

        private async void LoadBlocks()
        {
            _blocks = new ObservableCollection<Block>();
            await foreach (var block in _blockRepository.GetAllBlocks())
            {
                    Blocks.Add(block);
            }
        }

        private async void LoadCourses()
        {
            _courses = new ObservableCollection<Course>();
            await foreach (var course in _courseRepository.GetAllCourses())
            {
                    Courses.Add(course);
            }
        }

        private async void LoadBlocksWithStudents()
        {
            ObservableCollection<StudentGrade> studlist;
            _blocksWithStudents = new ObservableCollection<BlockWithStudent>();
            await foreach(var blockHandled in _blockRepository.GetBlocksHandledByUser())
            {
                studlist = new ObservableCollection<StudentGrade>();
                await foreach (var student in _studentRepository.GetStudentsByBlock(blockHandled.Id))
                {
                    studlist.Add(student);
                    
                    Debug.WriteLine(BlocksWithStudents);
                }
                var temp = new BlockWithStudent
                {
                    Block = new Block { Name = blockHandled.Name, Id = blockHandled.Id },
                    StudentsWithGrade = new ObservableCollection<StudentGrade>(studlist)
                };
                BlocksWithStudents.Add(temp);


            }
        }

        [RelayCommand]
        public void TriggerAddModal()
        {
            IsAddModalVisible = !IsAddModalVisible;
        }

        [RelayCommand]
        public void AddStudents()
        {
            _addStudentsService.AddStudents(SelectedBlock, SelectedCourse);
            
            TriggerAddModal();
        }
    }
    public record BlockWithStudent()
    {
        public Block Block { get; internal set; }
        public ObservableCollection<StudentGrade> StudentsWithGrade { get; internal set; }
    }
}