using ExchangeRates.Repositories;
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
        private readonly IExchangeRatesRepository exchangeRatesRepository = new ExchangeRatesRepository();
        
        public List<Currency> fillCB()
        {
            List<Currency> allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            return allMyCurrencies;
        }

        public List<ExchangeRate> loadTable()
        {
            return exchangeRatesRepository.GetAllExchangeRates();
        }

        public string insert(DateTime? SelectedDate, Currency from, Currency to, string Rate, bool? IsChecked)
        {
            if (SelectedDate == null || SelectedDate.Value.Date == null || from == null || to == null || Rate == "")
            {
                return "Please fill out all fields.";
            }
            else 
            {
                int result = DateTime.Compare(SelectedDate.Value.Date, DateTime.Today);
                if(result < 0)
                {
                    return "Please select a later date.";
                }
                else
                {
                    ExchangeRate er = new ExchangeRate();
                    er.ValidityDate = SelectedDate.Value.Date;
                    er.CurrencyFrom = from.CurrencyId;
                    er.CurrencyTo = to.CurrencyId;
                    er.Rate = int.Parse(Rate);
                    if (IsChecked == true)
                    {
                        er.IsActive = 1;
                    }
                    else
                    {
                        er.IsActive = 0;
                    }

                    return exchangeRatesRepository.InsertExchangeRate(er);
                }
            }            
        }

        public string edit(ExchangeRate er, DateTime? SelectedDate, Currency from, Currency to, string Rate, bool? IsChecked)
        {
            if (SelectedDate == null || SelectedDate.Value.Date == null || from == null || to == null || Rate == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                er.ValidityDate = SelectedDate.Value.Date;
                er.CurrencyFrom = from.CurrencyId;
                er.CurrencyTo = to.CurrencyId;
                er.Rate = int.Parse(Rate);
                if (IsChecked == true)
                {
                    er.IsActive = 1;
                }
                else
                {
                    er.IsActive = 0;
                }

                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }

        public void delete(ExchangeRate er)
        {
            exchangeRatesRepository.DeleteExchangeRate(er);
        }
    }
}
