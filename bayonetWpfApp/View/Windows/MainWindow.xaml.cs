using bayonetWpfApp.Models;
using bayonetWpfApp.View.Pages;
using System.Windows;

namespace bayonetWpfApp
{
    public partial class MainWindow : Window
    {
        private User currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadNewsPage(); 
        }

        private void LoadNewsPage()
        {
          
            NewsPage newsPage = new NewsPage();
            MainFrm.Navigate(newsPage);
        }

        private void ResheniaBtn_Click(object sender, RoutedEventArgs e)
        {
            NewsPage newsPage = new NewsPage();
            MainFrm.Navigate(newsPage);
        }

        private void LearningBtn_Click(object sender, RoutedEventArgs e)
        {
           
                ProfilPage profilPage = new ProfilPage(currentUser);
                MainFrm.NavigationService.Navigate(profilPage);
            
        }



        private void ProfilBtn_Click(object sender, RoutedEventArgs e)
        {
            SchedulePage schedulePage = new SchedulePage();
            MainFrm.Navigate(schedulePage);
        }
    }
}
