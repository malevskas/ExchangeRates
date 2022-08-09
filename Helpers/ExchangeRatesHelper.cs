using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class ExchangeRatesHelper
    {
        private Entity myExchangeDatabase = new Entity();
        private Label ExchangeRatesId;
        private TextBox Rate;
        private ComboBox CurrencyFromCB;
        private ComboBox CurrencyToCB;
        private DatePicker ValidityDate;
        private CheckBox checkBox;
        private DataGrid dataGrid;

        public ExchangeRatesHelper(Label ExchangeRatesId, TextBox Rate, ComboBox CurrencyFromCB, ComboBox CurrencyToCB, DatePicker ValidityDate, CheckBox checkBox, DataGrid dataGrid)
        {
            this.ExchangeRatesId = ExchangeRatesId;
            this.Rate = Rate;
            this.CurrencyFromCB = CurrencyFromCB;
            this.CurrencyToCB = CurrencyToCB;
            this.ValidityDate = ValidityDate;
            this.checkBox = checkBox;
            this.dataGrid = dataGrid;
        }

        public void fillCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyFromCB.ItemsSource = allMyCurrencies;
            CurrencyToCB.ItemsSource = allMyCurrencies;
        }

        public void loadTable()
        {
            var allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            dataGrid.ItemsSource = allMyExchangeRates;
        }

        public void insert()
        {
            int result = DateTime.Compare(ValidityDate.SelectedDate.Value.Date, DateTime.Today);
            if (ValidityDate.SelectedDate.Value.Date == null || CurrencyFromCB.SelectedItem == null || CurrencyToCB.SelectedItem == null || Rate.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else if (result < 0)
            {
                MessageBox.Show("Please select a later date.");
            }
            /*else if(CurrencyFromCB.SelectedItem == CurrencyToCB.SelectedItem)
            {
                MessageBox.Show("Please choose different currencies.");
            }*/
            else
            {
                ExchangeRate er = new ExchangeRate();
                er.ValidityDate = ValidityDate.SelectedDate.Value.Date;
                Currency c = (Currency)CurrencyFromCB.SelectedItem;
                er.CurrencyFrom = c.CurrencyId;
                c = (Currency)CurrencyToCB.SelectedItem;
                er.CurrencyTo = c.CurrencyId;
                er.Rate = int.Parse(Rate.Text);
                if (checkBox.IsChecked == true)
                {
                    er.IsActive = 1;
                }
                else
                {
                    er.IsActive = 0;
                }

                myExchangeDatabase.ExchangeRates.Add(er);
                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void edit()
        {
            if (ValidityDate.SelectedDate.Value.Date == null || CurrencyFromCB.SelectedItem == null || CurrencyToCB.SelectedItem == null || Rate.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else
            {
                ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
                er.ValidityDate = ValidityDate.SelectedDate.Value.Date;
                Currency c = (Currency)CurrencyFromCB.SelectedItem;
                er.CurrencyFrom = c.CurrencyId;
                c = (Currency)CurrencyToCB.SelectedItem;
                er.CurrencyTo = c.CurrencyId;
                er.Rate = int.Parse(Rate.Text);
                if (checkBox.IsChecked == true)
                {
                    er.IsActive = 1;
                }
                else
                {
                    er.IsActive = 0;
                }

                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void delete()
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            er.IsActive = 0;
            checkBox.IsChecked = false;
            myExchangeDatabase.SaveChanges();

            var allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            dataGrid.ItemsSource = allMyExchangeRates;
        }

        public void populateTextBox()
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            ExchangeRatesId.Content = er.ExchangeRatesId.ToString();
            ValidityDate.SelectedDate = er.ValidityDate;
            Rate.Text = er.Rate.ToString();
            CurrencyFromCB.Text = er.Currency.CurrencyName;
            CurrencyToCB.Text = er.Currency1.CurrencyName;
            if (er.IsActive == 1)
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
