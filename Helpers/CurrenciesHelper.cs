using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class CurrenciesHelper
    {
        private Entity myExchangeDatabase = new Entity();
        private Label CurrencyId;
        private TextBox Code;
        private TextBox CurrencyName;
        private CheckBox checkBox;
        private DataGrid dataGrid;

        public CurrenciesHelper(Label CurrencyId, TextBox Code, TextBox CurrencyName, CheckBox checkBox, DataGrid dataGrid)
        {
            this.CurrencyId = CurrencyId;
            this.Code = Code;
            this.CurrencyName = CurrencyName;
            this.checkBox = checkBox;
            this.dataGrid = dataGrid;
        }

        public void loadTable()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            dataGrid.ItemsSource = allMyCurrencies;
        }

        public void insert()
        {
            Currency currency = new Currency();
            currency.Code = Code.Text;
            currency.CurrencyName = CurrencyName.Text;
            if (checkBox.IsChecked == true)
            {
                currency.IsActive = 1;
            }
            else
            {
                currency.IsActive = 0;
            }

            myExchangeDatabase.Currencies.Add(currency);
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void edit()
        {
            Currency currency = (Currency)dataGrid.SelectedItem;
            currency.Code = Code.Text;
            currency.CurrencyName = CurrencyName.Text;
            if (checkBox.IsChecked == true)
            {
                currency.IsActive = 1;
            }
            else
            {
                currency.IsActive = 0;
            }

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void delete()
        {
            Currency currency = (Currency)dataGrid.SelectedItem;
            currency.IsActive = 0;
            checkBox.IsChecked = false;
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void populateTextBox()
        {
            Currency currency = (Currency)dataGrid.SelectedItem;
            CurrencyId.Content = currency.CurrencyId.ToString();
            Code.Text = currency.Code;
            CurrencyName.Text = currency.CurrencyName;
            if (currency.IsActive == 1)
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
