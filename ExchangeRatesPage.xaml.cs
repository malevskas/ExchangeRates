using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Interaction logic for ExchangeRatesPage.xaml
    /// </summary>
    public partial class ExchangeRatesPage : Page
    {
        public ExchangeRatesPage()
        {
            InitializeComponent();
            fillCB();
        }
        
        private Entity myExchangeDatabase = new Entity();

        void fillCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyFromCB.Items.Clear();
            CurrencyToCB.Items.Clear();
            foreach (var currency in allMyCurrencies)
            {
                CurrencyFromCB.Items.Add(currency.CurrencyId);
                CurrencyToCB.Items.Add(currency.CurrencyId);
            }
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            var allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            dataGrid.ItemsSource = allMyExchangeRates;
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            int isActive = 0;
            if (checkBox.IsChecked == true)
            {
                isActive = 1;
            }

            //ExchangeRate er = new ExchangeRate();
            //er.ValidityDate = ValidityDate.Text;
            //er.CurrencyFrom = CurrencyFromCB.Text;
            //er.CurrencyTo = CurrencyToCB.Text;
            //er.Rate = Rate.Text;
            //er.IsActive = isActive;

            //SqlConnection conn = new SqlConnection(cs);
            //string comm = "EXEC InsertIntoExchangeRates @ValidityDate = '" + ValidityDate.Text + "', @CurrencyFrom = " +
            //    CurrencyFromCB.Text + ", @CurrencyTo = " + CurrencyToCB.Text + ", @Rate = " + Rate.Text + 
            //    ", @IsActive = " + isActive;

            //try
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand(comm, conn);
            //    cmd.ExecuteNonQuery();
            //    cmd = new SqlCommand("SELECT * FROM ExchangeRates", conn);
            //    cmd.ExecuteNonQuery();
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable("Exchange Rates");
            //    adapter.Fill(dt);
            //    dataGrid.ItemsSource = dt.DefaultView;
            //    adapter.Update(dt);
            //}
            //catch (Exception ex)
            //{
            //    conn.Close();
            //}
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            er.IsActive = 0;
            myExchangeDatabase.SaveChanges();

            var allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            dataGrid.ItemsSource = allMyExchangeRates;
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            ExchangeRatesId.Text = er.ExchangeRatesId + "";
            ValidityDate.Text = er.ValidityDate.ToString();
            Rate.Text = er.Rate.ToString();
            CurrencyFromCB.Text = er.CurrencyFrom.ToString();
            CurrencyToCB.Text = er.CurrencyTo.ToString();
            if (er.IsActive == 1)
            {
                checkBox.IsChecked = true;
            }
        }
    }
}
