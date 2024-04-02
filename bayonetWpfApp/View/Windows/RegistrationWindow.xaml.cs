using bayonetWpfApp.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity.Validation;

namespace bayonetWpfApp.View.Windows
{
    public partial class RegistrationWindow : Window
    {
        private const int MaxLoginLength = 20; 
        private const int MaxPasswordLength = 15; 
        private const int MaxNameLength = 30; 

        public RegistrationWindow()
        {
            InitializeComponent();
            FillGenderComboBox();
            FirstNameTb.TextChanged += FirstNameTb_TextChanged;
            LastNameTb.TextChanged += LastNameTb_TextChanged;
            MiddleNameTb.TextChanged += MiddleNameTb_TextChanged;
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string login = LoginTb.Text;
            string password = PasswordPb.Password;
            string repeatPassword = RepeatPasswordPb.Password;
            string selectedGenderName = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime? birthDate = BirthDatePicker.SelectedDate;
            string firstName = FirstNameTb.Text;
            string lastName = LastNameTb.Text;
            string middleName = MiddleNameTb.Text;

            
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(repeatPassword) || string.IsNullOrWhiteSpace(selectedGenderName) ||
                !birthDate.HasValue || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
            if (firstName.Length > MaxNameLength || lastName.Length > MaxNameLength || middleName.Length > MaxNameLength)
            {
                MessageBox.Show($"Имя, фамилия и отчество не должны превышать {MaxNameLength} символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

       
            Gender selectedGender = null;
            try
            {
                using (var context = new KnifeDBEntities())
                {
                    selectedGender = context.Gender.FirstOrDefault(g => g.GenderName == selectedGenderName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении объекта Gender: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (selectedGender == null)
            {
                MessageBox.Show("Выбран недопустимый пол.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        
            if (birthDate.Value > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть позже текущей даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
            User newUser = new User
            {
                Login = login,
                Password = password,
                Gender = selectedGender,
                BirthDate = birthDate.Value,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName
            };

          
            try
            {
                using (var context = new KnifeDBEntities())
                {
                    App.enteredUser = newUser;
                    context.User.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Регистрация успешно завершена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow main = new MainWindow(newUser);
                main.Show();
                Close(); 
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Ошибка валидации: {validationError.ErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillGenderComboBox()
        {
            try
            {
                using (var context = new KnifeDBEntities())
                {
                 
                    var genders = context.Gender.ToList();

                   
                    GenderComboBox.ItemsSource = genders.Select(g => new ComboBoxItem { Content = g.GenderName });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при заполнении ComboBox данными: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z0-9_]+$");
        }

        private void PasswordPb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z0-9`~!@#$%^&*()_+-=\[\]{};':"",.<>/?]+$"))
            {
                e.Handled = true; 
            }
        }

        private void RepeatPasswordPb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z0-9`~!@#$%^&*()_+-=\[\]{};':"",.<>/?]+$"))
            {
                e.Handled = true; 
            }
        }

        private void LoginTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTb.Text.Length > MaxLoginLength)
            {
                
                LoginTb.Text = LoginTb.Text.Substring(0, MaxLoginLength);
               
                LoginTb.CaretIndex = MaxLoginLength;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }

        private void PasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPb.Password.Length > MaxPasswordLength)
            {
                
                PasswordPb.Password = PasswordPb.Password.Substring(0, MaxPasswordLength);
               
                PasswordPb.SelectAll();
            }
        }
        private void FirstNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length > MaxNameLength)
            {
                
                textBox.Text = textBox.Text.Substring(0, MaxNameLength);
            
                textBox.CaretIndex = MaxNameLength;
            }
        }

        private void LastNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length > MaxNameLength)
            {
                
                textBox.Text = textBox.Text.Substring(0, MaxNameLength);
               
                textBox.CaretIndex = MaxNameLength;
            }
        }

        private void MiddleNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length > MaxNameLength)
            {
                
                textBox.Text = textBox.Text.Substring(0, MaxNameLength);
                
                textBox.CaretIndex = MaxNameLength;
            }
        }

        private void RepeatPasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepeatPasswordPb.Password.Length > MaxPasswordLength)
            {
                RepeatPasswordPb.Password = RepeatPasswordPb.Password.Substring(0, MaxPasswordLength);
   
                RepeatPasswordPb.SelectAll();
            }
        }
    }
}
