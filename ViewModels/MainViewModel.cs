using Lab3_Programming.Models;
using Lab3_Programming.Repositories;
using Lab3_Programming.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab3_Programming.ViewModels
{
    public class MainViewModel
    {
        private readonly IStudentRepository _repository;
        public ObservableCollection<Student> Students { get; set; } = new();

        public ICommand AddCommand { get; }
        public ICommand StudentTappedCommand { get; } 
        public MainViewModel(IStudentRepository repository)
        {
            _repository = repository;
            AddCommand = new Command(async () => await Shell.Current.Navigation.PushAsync(new StudentDetailsPage(_repository)));
            
            StudentTappedCommand = new Command<Student>(async (student) => await HandleStudentTapped(student));
        }
        private async Task HandleStudentTapped(Student student)
        {
            if (student == null) return;

            var action = await Application.Current.MainPage.DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            if (action == "Edit")
            {
                await Shell.Current.Navigation.PushAsync(new StudentDetailsPage(_repository, student));
            }
            else if (action == "Delete")
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmation", "Are you sure you want to delete this student?", "Yes", "No");
                if (confirm)
                {
                    await _repository.Delete(student);
                    await LoadStudents();
                }
            }
        }

        public async Task LoadStudents()
        {
            var list = await _repository.GetStudents();
            Students.Clear();
            foreach (var student in list)
            {
                Students.Add(student);
            }
        }
    }
}