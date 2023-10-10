using Microsoft.EntityFrameworkCore;
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
using users.Entities;

namespace Auth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private user_dbContext dbContext;
        public MainWindow()
        {
            InitializeComponent();
            dbContext = new user_dbContext();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Window3 Reg = new Window3();
            Reg.Show();
            this.Close();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string Login = LogTxT.Text;
            string Password = InviseblePSD.Password;

            var user = dbContext.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);
            if (user != null)
            {
                if (dbContext.Roles.FirstOrDefault(r => r.RoleName == "Администратор").Id == user.IdRole)
                {
                    Window1 adminWindow = new Window1();
                    adminWindow.Show();
                    this.Close();
                }
                else if (dbContext.Roles.FirstOrDefault(r => r.RoleName == "Пользователь").Id == user.IdRole)
                {
                    Window2 userWindow = new Window2();
                    userWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void LogTxT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where
                         (ch =>
                            ch == '_' || ch == '-'
                            || (ch >= '0' && ch <= '9')
                            || (ch >= 'a' && ch <= 'z')
                            || (ch >= 'A' && ch <= 'Z')
                         )
                         .ToArray()
                    );
            }
        }

        private void Showbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Showbtn.Content.ToString() == "Показать пароль")
            {
                string password = InviseblePSD.Password;
                ShowPSD.Text = password;
                ShowPSD.Visibility = Visibility.Visible;
                InviseblePSD.Password = ShowPSD.Text;
                InviseblePSD.Visibility = Visibility.Collapsed;
                Showbtn.Content = "Скрыть пароль";
            }
            else
            {
                InviseblePSD.Visibility = Visibility.Visible;
                InviseblePSD.Password = ShowPSD.Text;
                ShowPSD.Visibility = Visibility.Collapsed;
                Showbtn.Content = "Показать пароль";
            }
        }
    }
}
