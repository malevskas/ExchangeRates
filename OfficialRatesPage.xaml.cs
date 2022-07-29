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
    /// Interaction logic for OfficialRatesPage.xaml
    /// </summary>
    public partial class OfficialRatesPage : Page
    {
        public OfficialRatesPage()
        {
            InitializeComponent();
            fillCB();
        }

        private Entity myExchangeDatabase = new Entity();

        void fillCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyCB.Items.Clear();

            foreach (var currency in allMyCurrencies)
            {
                CurrencyCB.Items.Add(currency.CurrencyId);
            }
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            var allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();
            dataGrid.ItemsSource = allMyOfficialRates;
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            int isActive = 0;
            if (checkBox.IsChecked == true)
            {
                isActive = 1;
            }
            /*SqlConnection conn = new SqlConnection(cs);
            string comm = "EXEC InsertIntoOfficialRates @ValidityDate = '" + ValidityDate.Text + "', @Currency = " +
                CurrencyCB.Text + ", @Rate = " + Rate.Text + ", @IsActive = " + isActive;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("SELECT * FROM OfficialRates", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Official Rates");
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                conn.Close();
            }*/
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            or.IsActive = 0;
            myExchangeDatabase.SaveChanges();

            var allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();
            dataGrid.ItemsSource = allMyOfficialRates;
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }
    }
}
