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

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            string cs = "Data Source=DESKTOP-JRGOK04;Initial Catalog=ExchangeRatesDB;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            string comm = "SELECT * FROM Users";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Users");
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            int isActive = 0;
            if (checkBox.IsChecked == true)
            {
                isActive = 1;
            }
            string cs = "Data Source=DESKTOP-JRGOK04;Initial Catalog=ExchangeRatesDB;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            string comm = "EXEC InsertIntoUers @FirstName = '" + FirstName.Text + "', @Surname = '" +
                LastName.Text + "', @IsActive = " + isActive;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("SELECT * FROM Users", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Users");
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }
    }
}
