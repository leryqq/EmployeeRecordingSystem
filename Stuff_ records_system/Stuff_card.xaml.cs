using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static System.Net.Mime.MediaTypeNames;

namespace Stuff__records_system
{
    /// <summary>
    /// Логика взаимодействия для Stuff_card.xaml
    /// </summary>
    public partial class Stuff_card : Window
    {
        public Stuff_card()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source = DESKTOP-VR5EFC4; Initial Catalog = LoginDB; Integrated Security = True");

        public bool isValid()
        {
            if (name_txt.Text == string.Empty || gender_txt.Text == string.Empty || birthplace_txt.Text == string.Empty || birthdaydate_picker.Text == string.Empty || enterdate_picker.Text == string.Empty || phone_txt.Text == string.Empty || email_txt.Text == string.Empty || numberofcontract_txt.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbStuff VALUES (@Name, @Gender, @BirthPlace, @BirthDate, @EnterDate, @Telephone, @Email, @NumberOfContract)", sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender_txt.Text);
                    cmd.Parameters.AddWithValue("@BirthPlace", birthplace_txt.Text);
                    cmd.Parameters.AddWithValue("@BirthDate", birthdaydate_picker.Text);
                    cmd.Parameters.AddWithValue("@EnterDate", enterdate_picker.Text);
                    cmd.Parameters.AddWithValue("@Telephone", phone_txt.Text);
                    cmd.Parameters.AddWithValue("@Email", email_txt.Text);
                    cmd.Parameters.AddWithValue("@NumberOfContract", numberofcontract_txt.Text);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Данные успешно сохранены!", "Сохранено!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
