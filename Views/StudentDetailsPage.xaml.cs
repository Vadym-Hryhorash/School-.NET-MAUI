using Lab3_Programming.Models;
using Lab3_Programming.Repositories;
using Lab3_Programming.ViewModels;

namespace Lab3_Programming.Views
{
    public partial class StudentDetailsPage : ContentPage
    {
        public StudentDetailsPage(IStudentRepository repository, Student student = null)
        {
            InitializeComponent();
            BindingContext = new StudentDetailsViewModel(repository, student);
        }
    }
}
