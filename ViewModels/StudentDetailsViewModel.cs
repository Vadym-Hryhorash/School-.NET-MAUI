using Lab3_Programming.Models;
using Lab3_Programming.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Lab3_Programming.ViewModels
{
    public class StudentDetailsViewModel : BaseViewModel
    {
        private readonly IStudentRepository _repository;
        private Student _currentStudent;
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _lastName;
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        private string _thirdName;
        public string ThirdName { get => _thirdName; set => SetProperty(ref _thirdName, value); }
        private string _gradeText;
        public string GradeText { get => _gradeText; set => SetProperty(ref _gradeText, value); }
        private string _phone;
        public string ParentsPhone { get => _phone; set => SetProperty(ref _phone, value); }
        private string _subject;
        public string FavoriteSubject { get => _subject; set => SetProperty(ref _subject, value); }
        public ICommand SaveCommand { get; }

        public StudentDetailsViewModel(IStudentRepository repository, Student student = null)
        {
            _repository = repository;
            _currentStudent = student;

            if (_currentStudent != null)
            {
                Name = _currentStudent.Name;
                LastName = _currentStudent.LastName;
                ThirdName = _currentStudent.ThirdName;
                GradeText = _currentStudent.AvarageGrade.ToString();
                ParentsPhone = _currentStudent.ParentsPhone;
                FavoriteSubject = _currentStudent.FavoriteSubject;
            }

            SaveCommand = new Command(async () => await Save());
        }

        private async Task Save()
        {
            string validName = Name?.Trim();
            string validLastName = LastName?.Trim();

            if (string.IsNullOrWhiteSpace(validName) || string.IsNullOrWhiteSpace(validLastName))
            {
                await App.Current.MainPage.DisplayAlert("Warning",
                    "Name and Last name are required!", "Ok");
                return;
            }
            double grade = 0;
            string validGradeText = GradeText?.Trim()?.Replace(",", ".");

            if (!string.IsNullOrWhiteSpace(validGradeText))
            {
                if (!double.TryParse(validGradeText, out grade))
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Invalid format for Average Grade!", "Ok");
                    return;
                }

                if (grade < 0 || grade > 12)
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Average grade must be between 0 and 12.", "Ok");
                    return;
                }
            }

            string validPhone = ParentsPhone?.Trim();
            if (!string.IsNullOrWhiteSpace(validPhone))
            {
                if (validPhone.Length < 10 || validPhone.Length > 13)
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Phone number format: +38**********", "Ok");
                    return;
                }
            }
            try
            {
                if (_currentStudent == null)
                {
                    await _repository.Create(new Student
                    {
                        Name = validName,
                        LastName = validLastName,
                        ThirdName = ThirdName?.Trim(),
                        AvarageGrade = grade,
                        ParentsPhone = validPhone,
                        FavoriteSubject = FavoriteSubject?.Trim()
                    });
                }
                else
                {
                    _currentStudent.Name = validName;
                    _currentStudent.LastName = validLastName;
                    _currentStudent.ThirdName = ThirdName?.Trim();
                    _currentStudent.AvarageGrade = grade;
                    _currentStudent.ParentsPhone = validPhone;
                    _currentStudent.FavoriteSubject = FavoriteSubject?.Trim();

                    await _repository.Update(_currentStudent);
                }

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occurred while saving to the database. Please try again.", "Ok");
            }
        }
    }
}
