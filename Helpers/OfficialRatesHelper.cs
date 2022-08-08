using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class OfficialRatesHelper
    {
        private Entity myExchangeDatabase = new Entity();
        private Label OfficialRatesId;
        private ComboBox CurrencyCB;
        private TextBox Rate;
        private DatePicker ValidityDate;
        private CheckBox checkBox;
        private DataGrid dataGrid;

        public OfficialRatesHelper(Label OfficialRatesId, ComboBox CurrencyCB, TextBox Rate, DatePicker ValidityDate, CheckBox checkBox, DataGrid dataGrid)
        {
            this.OfficialRatesId = OfficialRatesId;
            this.CurrencyCB = CurrencyCB;
            this.Rate = Rate;
            this.ValidityDate = ValidityDate;
            this.checkBox = checkBox;
            this.dataGrid = dataGrid;
        }

        public void fillCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyCB.Items.Clear();
            CurrencyCB.ItemsSource = allMyCurrencies;
        }

        public void loadTable()
        {
            var allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();
            dataGrid.ItemsSource = allMyOfficialRates;
        }

        public void insert()
        {
            var allMyCurrencies = myExchangeDatabase.OfficialRates.Select(x => x.Currency1);
            if (ValidityDate.SelectedDate.Value.Date == null || CurrencyCB.SelectedItem == null || Rate.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else if(allMyCurrencies.Contains((Currency)CurrencyCB.SelectedItem))
            {
                //MessageBoxButton mbb = MessageBoxButton.YesNo;
                //MessageBoxImage icon = MessageBoxImage
                //MessageBox.Show("An official rate with that name alreay exists. Would you like to update it?", "", mbb, );
            }
            else
            {
                OfficialRate or = new OfficialRate();
                or.ValidityDate = ValidityDate.SelectedDate.Value.Date;
                Currency c = (Currency)CurrencyCB.SelectedItem;
                or.Currency = c.CurrencyId;
                or.Rate = int.Parse(Rate.Text);
                if (checkBox.IsChecked == true)
                {
                    or.IsActive = 1;
                }
                else
                {
                    or.IsActive = 0;
                }

                myExchangeDatabase.OfficialRates.Add(or);
                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void edit()
        {
            if (ValidityDate.SelectedDate.Value.Date == null || CurrencyCB.SelectedItem == null || Rate.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else
            {
                OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
                or.ValidityDate = ValidityDate.SelectedDate.Value.Date;
                Currency c = (Currency)CurrencyCB.SelectedItem;
                or.Currency = c.CurrencyId;
                or.Rate = int.Parse(Rate.Text);
                if (checkBox.IsChecked == true)
                {
                    or.IsActive = 1;
                }
                else
                {
                    or.IsActive = 0;
                }

                myExchangeDatabase.OfficialRates.Add(or);
                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void delete()
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            or.IsActive = 0;
            checkBox.IsChecked = false;

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void populateTextBox()
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            OfficialRatesId.Content = or.OfficialRatesId.ToString();
            ValidityDate.SelectedDate = or.ValidityDate;
            Rate.Text = or.Rate.ToString();
            CurrencyCB.Text = or.Currency1.CurrencyName;
            if (or.IsActive == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
    }
}
