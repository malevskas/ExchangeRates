using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class CurrenciesHelper
    {
        private Entity myExchangeDatabase = new Entity();

        public List<Currency> loadTable()
        {
            List<Currency> allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            return allMyCurrencies;
        }

        public string insert(string CodeText, string CurrencyNameText, bool? IsChecked)
        {
            var allMyCodes = myExchangeDatabase.Currencies.Select(x => x.Code);
            var allMyCurrencyNames = myExchangeDatabase.Currencies.Select(x => x.CurrencyName);

            if (CodeText == "" || CurrencyNameText == "")
            {
                return "Please fill out all fields.";
            }
            else if (allMyCodes.Contains(CodeText))
            {
                return "A currency with that code already exists.";
            }
            else if(allMyCurrencyNames.Contains(CurrencyNameText))
            {
                return "A currency with that name already exists.";
            }
            else
            {
                Currency currency = new Currency();
                currency.Code = CodeText;
                currency.CurrencyName = CurrencyNameText;
                if (IsChecked == true)
                {
                    currency.IsActive = 1;
                }
                else
                {
                    currency.IsActive = 0;
                }

                myExchangeDatabase.Currencies.Add(currency);
                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }

        public string edit(string CodeText, string CurrencyNameText, bool? IsChecked)
        {
            if (CodeText == "" || CurrencyNameText == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                Currency currency = (Currency)dataGrid.SelectedItem;
                currency.Code = CodeText;
                currency.CurrencyName = CurrencyNameText;
                if (IsChecked == true)
                {
                    currency.IsActive = 1;
                }
                else
                {
                    currency.IsActive = 0;
                }

                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }

        public void delete(Currency currency)
        {
            currency.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }
    }
}
