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
using System.Data.SqlClient;
using System.Windows.Markup;
using System.Data;

namespace Stuff__records_system
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source = DESKTOP-VR5EFC4; Initial Catalog = LoginDB; Integrated Security = True");

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from tbStuff", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlConnection.Close();
            DGridStuff.ItemsSource = dataTable.DefaultView;
        }

        
        public void TextBoxFilter()
        {
            string text = searchTextBox.Text;
            SqlCommand cmd = new SqlCommand("select * from tbStuff where Name = '"+text+ "'", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            sqlConnection.Close();
            DGridStuff.ItemsSource = dataTable.DefaultView;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from tbStuff", sqlConnection);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Данные удалены", "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);
                sqlConnection.Close();
            } 
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { 
                sqlConnection.Close();
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Stuff_card a = new Stuff_card();
            a.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFilter();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }
    }
}
