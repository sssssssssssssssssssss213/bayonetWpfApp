using bayonetWpfApp.Models;
using System;
using System.Linq;
using System.Windows;

namespace bayonetWpfApp.View.Windows
{
    public partial class RemovePasswordWindow : Window
    {
        public RemovePasswordWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
        
            string login = LoginTb.Text;
            string password = PasswordPb.Password;
            string repeatPassword = RepeatPasswordPb.Password;


            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repeatPassword))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

          
            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new KnifeDBEntities())
                {
                  
                    var user = context.User.FirstOrDefault(u => u.Login == login);

                   
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь с указанным логином не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

            
                    user.Password = password;

                   
                    context.SaveChanges();
                }

                MessageBox.Show("Пароль успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении пароля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
        }
    }
}
