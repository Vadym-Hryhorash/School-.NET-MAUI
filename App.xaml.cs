using Lab3_Programming.Views;

namespace Lab3_Programming
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(mainPage);
        }
    }
}
