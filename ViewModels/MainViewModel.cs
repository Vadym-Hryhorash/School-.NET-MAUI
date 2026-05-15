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
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public MainViewModel(IStudentRepository repository)
        {
            _repository = repository;
            AddCommand = new Command(async () => await Shell.Current.Navigation.PushAsync(new StudentDetailsPage(_repository)));
            EditCommand = new Command<Student>(async (s) => await Shell.Current.Navigation.PushAsync(new StudentDetailsPage(_repository, s)));
            DeleteCommand = new Command<Student>(async (s) =>
            {
                await _repository.Delete(s);
                await LoadStudents();
            });
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
