using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CurrenciesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CurrenciesPage();
        }
        private void ExchangeRatesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ExchangeRatesPage();
        }
        private void OfficialRatesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OfficialRate();
        }
        private void OperationsMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OperationsPage();
        }
        private void OperationTypesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OperationTypesPage();
        }
        private void UsersMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UsersPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cs = "Data Source=DESKTOP-JRGOK04;Initial Catalog=ExchangeRatesDB;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            string comm = "EXEC UpdateUsers @Id = 1, @FirstName = 'Simona', @Surname = 'Malevska', @IsActive = 0";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();

            } catch (Exception ex)
            {
                conn.Close();
            }
        }
    }
}
