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
using System.Text.RegularExpressions;
using ExchangeRates.Helpers;
using System.Diagnostics;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for OfficialRatesPage.xaml
    /// </summary>
    public partial class OfficialRatesPage : Page
    {
        private OfficialRatesHelper orHelper;

        public OfficialRatesPage()
        {
            InitializeComponent();
            orHelper = new OfficialRatesHelper(OfficialRatesId, CurrencyCB, Rate, ValidityDate, checkBox, dataGrid);
            orHelper.fillCB();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {   
            nbrm.Kurs kurs = new nbrm.Kurs();
            
            var result = kurs.GetExchangeRate("01.02.2010", "15.02.2010");
            Debug.Write(result.ToString());
            //orHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            orHelper.insert();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            orHelper.edit();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            orHelper.delete();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            orHelper.populateTextBox();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
