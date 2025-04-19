using _2kvalikSemenova.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2kvalikSemenova.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PassordPb.Password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Ищем пользователя по логину и паролю
            User loggedUser = App.db.User.FirstOrDefault(user =>
                user.Login == LoginTb.Text && user.Password == PassordPb.Password);

            if (loggedUser != null)
            {
                // Сохраняем текущего пользователя в приложении
                App.loggedUser = loggedUser;

                // Определяем роль пользователя на основе Role_Id
                int roleId = (int)loggedUser.Role_Id;

                switch (roleId)
                {
                    case 1: // Администратор
                        App.MainFrame.Navigate(new TovarList());
                        break;
                    case 2: // Модератор
                        App.MainFrame.Navigate(new TovarList());
                        break;
                    case 3: // Мастер
                        App.MainFrame.Navigate(new TovarList());
                        break;
                    case 4: // Пользователь
                        App.MainFrame.Navigate(new TovarList());
                        break;
                    default:
                        MessageBox.Show("Неизвестная роль пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    }

  
