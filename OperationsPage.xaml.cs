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
    /// Interaction logic for OperationsPage.xaml
    /// </summary>
    public partial class OperationsPage : Page
    {
        public OperationsPage()
        {
            InitializeComponent();
            fillCurrencyCB();
            fillUserCB();
            fillOperationTypeCB();
        }

        private Entity myExchangeDatabase = new Entity();

        void fillCurrencyCB()
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

        void fillUserCB()
        {
            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            UserCB.Items.Clear();
            foreach (var user in allMyUsers)
            {
                UserCB.Items.Add(user.UserId);
            }
        }

        void fillOperationTypeCB()
        {
            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            OperationTypeCB.Items.Clear();
            foreach (var oTypes in allMyOperationTypes)
            {
                OperationTypeCB.Items.Add(oTypes.OperationTypeId);
            }
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            var allMyOperations = myExchangeDatabase.Operations.ToList<Operation>();
            dataGrid.ItemsSource = allMyOperations;
        }

        private void Insert(object sender, RoutedEventArgs e)
        { 
            string cs = "Data Source=DESKTOP-JRGOK04;Initial Catalog=ExchangeRatesDB;Integrated Security=True";
            SqlConnection conn = new SqlConnection(cs);
            string comm = "EXEC InsertIntoCurrencies @UserId = " + OperationId.Text + ", @OperationDate = '" +
                OperationDate.Text + "', @Amount = " + Amount.Text + "@CurrencyFrom = " + CurrencyFromCB.Text + 
                ", @CurrencyTo = " + CurrencyToCB.Text;

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
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            Operation operation = (Operation)dataGrid.SelectedItem;

        }
    }
}
