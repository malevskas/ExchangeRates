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
        
        string cs = "Data Source=DESKTOP-JRGOK04;Initial Catalog=ExchangeRatesDB;Integrated Security=True";

        void fillCB()
        {
            SqlConnection conn = new SqlConnection(cs);
            string comm = "SELECT * FROM Currencies";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                var dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    var id = dr.GetValue(0);
                    CurrencyFromCB.Items.Add(id);
                    CurrencyToCB.Items.Add(id);
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            string comm = "SELECT * FROM ExchangeRates";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exchange Rates");
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
            SqlConnection conn = new SqlConnection(cs);
            string comm = "EXEC InsertIntoExchangeRates @ValidityDate = '" + ValidityDate.Text + "', @CurrencyFrom = " +
                CurrencyFromCB.Text + ", @CurrencyTo = " + CurrencyToCB.Text + ", @Rate = " + Rate.Text + 
                ", @IsActive = " + isActive;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(comm, conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("SELECT * FROM ExchangeRates", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exchange Rates");
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
