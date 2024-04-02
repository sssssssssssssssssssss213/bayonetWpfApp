using bayonetWpfApp.Models;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;

namespace bayonetWpfApp.View.Pages
{
    public partial class ProfilPage : Page
    {
        private User currentUser;

        public ProfilPage(User enteredUser)
        {
            InitializeComponent();

            currentUser = enteredUser;

            // Заполнение данных пользователя
            FillUserData();
            FillUserDataAndAchievements();
            // Установка DataContext
            DataContext = currentUser;
        }

        private void FillUserData()
        {
            if (currentUser != null)
            {
                FirstNameTextBox.Text = currentUser.FirstName;
                LastNameTextBox.Text = currentUser.LastName;
                MiddleNameTextBox.Text = currentUser.MiddleName;

                BirthDatePicker.SelectedDate = currentUser.BirthDate;
            }
        }
        private void FillUserDataAndAchievements()
        {
            if (currentUser != null)
            {
                FirstNameTextBox.Text = currentUser.FirstName;
                LastNameTextBox.Text = currentUser.LastName;
                MiddleNameTextBox.Text = currentUser.MiddleName;

                BirthDatePicker.SelectedDate = currentUser.BirthDate;

                // Получение достижений пользователя из базы данных
                var userAchievements = App.context.UserAchievement
                    .Where(ua => ua.UserId == currentUser.UserId)
                    .Select(ua => new { ua.AchievementDate, ua.Achievement.AchievementName })
                    .ToList();

                // Привязка полученных достижений к ListView
                AchievementsListView.ItemsSource = userAchievements;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                using (var context = new KnifeDBEntities())
                {
                    User existingUser = context.User.FirstOrDefault(u => u.UserId == currentUser.UserId);

                    if (existingUser != null)
                    {
                        existingUser.FirstName = FirstNameTextBox.Text;
                        existingUser.LastName = LastNameTextBox.Text;
                        existingUser.MiddleName = MiddleNameTextBox.Text;
                        existingUser.BirthDate = BirthDatePicker.SelectedDate ?? DateTime.MinValue;

                        context.SaveChanges();

                        MessageBox.Show("Изменения сохранены успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка: пользователь не определен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


}
