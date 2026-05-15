using Lab3_Programming.Models;
using Lab3_Programming.Repositories;

namespace Lab3_Programming.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly IStudentRepository _studentRepository;
        private int _editStudentId;

        public MainPage(IStudentRepository studentRepository)
        {
            InitializeComponent();
            _studentRepository = studentRepository;
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await _studentRepository.GetStudents();
        }

        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentDetailsPage(_studentRepository, null));
        }
        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var student = (Student)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            try
            {
                switch (action)
                {
                    case "Edit":
                        await Navigation.PushAsync(new StudentDetailsPage(_studentRepository, student));
                        break;

                    case "Delete":
                        bool confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete this student?", "Yes", "No");
                        if (confirm)
                        {
                            await _studentRepository.Delete(student);
                            listView.ItemsSource = await _studentRepository.GetStudents();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error has occurred while processing the request. Please try again.", "Ok");
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}
