using System;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace Stuff__records_system
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-VR5EFC4;Initial Catalog=LoginDB;Integrated Security=True");

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM tbUser WHERE Login=@Login AND Password=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Login", txtLogin.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль неверный", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    } 
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { 
                sqlCon.Close();
            }
        }
    }
}
