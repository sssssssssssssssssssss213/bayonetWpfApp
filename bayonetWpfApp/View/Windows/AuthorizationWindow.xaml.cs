using bayonetWpfApp.Models;
using System;
using System.Linq;
using System.Windows;

namespace bayonetWpfApp.View.Windows
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text;
            string password = PasswordPb.Password;

        
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

       
            using (var db = new KnifeDBEntities())
            {
                var user = db.User.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    App.enteredUser = user;
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    Close();
                }
                else
                {
               
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
        }

        private void RegistretionBtn_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
     
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }

        private void PasswordRemove_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
  
            RemovePasswordWindow passwordRecoveryWindow = new RemovePasswordWindow();
            passwordRecoveryWindow.Show();
        }
    }
}
