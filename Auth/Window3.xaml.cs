using Microsoft.Data.SqlClient;
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
using System.Windows.Shapes;
using users.Entities;

namespace Auth
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            MainWindow Main1 = new MainWindow();
            Main1.Show();
            this.Close();
        }

        private void RegShow_Click(object sender, RoutedEventArgs e)
        {
            if (ZaregPass.Visibility == Visibility.Visible)
            {
                string password = ZaregPass.Password;
                ZaregPass.Visibility = Visibility.Collapsed;
                PassCheck.Visibility = Visibility.Visible;
                PassCheck.Text = password;
                RegShow.Content = "Скрыть пароль";
            }
            else
            {
                ZaregPass.Visibility = Visibility.Visible;
                PassCheck.Visibility = Visibility.Collapsed;
                RegShow.Content = "Показать пароль";
            }

        }

        string connectionString = "Data Source=DESKTOP-DG9H5PE\\SQLEXPRESS;Initial Catalog=user_db;Integrated Security=True";

        private void Zareg_Click(object sender, RoutedEventArgs e)
        {
            string login = ZaregTxT.Text;
            string password = ZaregPass.Password;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для вставки данных в базу данных
                string sqlQuery = "INSERT INTO dbo.Users (Login, Password, idRole) VALUES (@Login, @Password, 2)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    // Выполните SQL-запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Данные успешно добавлены
                        MessageBox.Show("Данные успешно добавлены в базу данных.");
                    }
                    else
                    {
                        // Что-то пошло не так
                        MessageBox.Show("Произошла ошибка при добавлении данных в базу данных.");
                    }
                }
            }
        }

        private void ZaregTxT_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
