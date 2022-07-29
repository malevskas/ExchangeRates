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
    /// Interaction logic for OperationTypesPage.xaml
    /// </summary>
    public partial class OperationTypesPage : Page
    {
        public OperationTypesPage()
        {
            InitializeComponent();
        }

        private Entity myExchangeDatabase = new Entity();

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            dataGrid.ItemsSource = allMyOperationTypes;
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
        private void Delete(object sender, RoutedEventArgs e)
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            ot.isActive = 0;
            myExchangeDatabase.SaveChanges();

            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            dataGrid.ItemsSource = allMyOperationTypes;
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            OperationTypeId.Text = ot.OperationTypeId.ToString();
            Code.Text = ot.Code;
            OperationName.Text = ot.OperationName;
            if (ot.isActive == 1)
            {
                checkBox.IsChecked = true;
            }
        }
    }
}
