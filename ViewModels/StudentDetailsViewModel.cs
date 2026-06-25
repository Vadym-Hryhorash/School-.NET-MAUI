using Lab3_Programming.Models;
using Lab3_Programming.Repositories;
using System.Windows.Input;

namespace Lab3_Programming.ViewModels
{
    public class StudentDetailsViewModel : BaseViewModel
    {
        private readonly IStudentRepository _repository;
        private Student? _currentStudent;

        private string _name = string.Empty;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private string _lastName = string.Empty;
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }

        private string? _thirdName;
        public string? ThirdName { get => _thirdName; set => SetProperty(ref _thirdName, value); }

        private string _gradeText = string.Empty;
        public string GradeText { get => _gradeText; set => SetProperty(ref _gradeText, value); }

        private string? _parentsPhone;
        public string? ParentsPhone { get => _parentsPhone; set => SetProperty(ref _parentsPhone, value); }

        private string? _favoriteSubject;
        public string? FavoriteSubject { get => _favoriteSubject; set => SetProperty(ref _favoriteSubject, value); }

        public ICommand SaveCommand { get; }

        public StudentDetailsViewModel(IStudentRepository repository, Student? student = null)
        {
            _repository = repository;
            _currentStudent = student;

            if (_currentStudent != null)
            {
                Name = _currentStudent.Name;
                LastName = _currentStudent.LastName;
                ThirdName = _currentStudent.ThirdName;
                GradeText = _currentStudent.AverageGrade?.ToString() ?? "";
                ParentsPhone = _currentStudent.ParentsPhone;
                FavoriteSubject = _currentStudent.FavoriteSubject;
            }

            SaveCommand = new Command(async () => await Save());
        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(LastName))
            {
                await ShowAlert("Warning", "Name and Last name are required!");
                return;
            }

            if (!double.TryParse(GradeText.Replace(",", "."), out double grade) || grade < 0 || grade > 12)
            {
                await ShowAlert("Warning", "Invalid grade! Please enter a value between 0 and 12.");
                return;
            }

            try
            {
                if (_currentStudent == null)
                {
                    await _repository.Create(new Student { 
                        Name = Name.Trim(), LastName = LastName.Trim(), 
                        ThirdName = ThirdName?.Trim(), AverageGrade = grade,
                        ParentsPhone = ParentsPhone?.Trim(), FavoriteSubject = FavoriteSubject?.Trim() 
                    });
                }
                else
                {
                    _currentStudent.Name = Name.Trim();
                    _currentStudent.LastName = LastName.Trim();
                    _currentStudent.ThirdName = ThirdName?.Trim();
                    _currentStudent.AverageGrade = grade;
                    _currentStudent.ParentsPhone = ParentsPhone?.Trim();
                    _currentStudent.FavoriteSubject = FavoriteSubject?.Trim();
                    await _repository.Update(_currentStudent);
                }

                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await ShowAlert("Error", "Database error. Please try again.");
            }
        }

        private Task ShowAlert(string title, string message)
        {
            return Application.Current!.MainPage!.DisplayAlert(title, message, "Ok");
        }
    }
}