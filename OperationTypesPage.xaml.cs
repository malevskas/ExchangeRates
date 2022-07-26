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

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for OperationTypesPage.xaml
    /// </summary>
    public partial class OperationTypesPage : Page
    {
        public OperationTypesPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
            /*string comm = "EXEC InsertIntoCurrencies @Code = '" + Code.Text + "', @CurrencyName = '" +
                CurrencyName.Text + "', @IsActive = " + isActive + ")";


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("SELECT * FROM Currencies", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Currencies");
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                conn.Close();
            }*/
        }
    }
}
