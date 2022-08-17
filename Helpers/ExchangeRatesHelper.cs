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
        private readonly IExchangeRatesRepository exchangeRatesRepository = new ExchangeRatesRepository();
        
        public List<Currency> fillCB()
        {
            return exchangeRatesRepository.GetAllCurrencies();
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
                ExchangeRate newER = new ExchangeRate();
                newER.ValidityDate = SelectedDate.Value.Date;
                newER.CurrencyFrom = from.CurrencyId;
                newER.CurrencyTo = to.CurrencyId;
                newER.Rate = int.Parse(Rate);
                if (IsChecked == true)
                {
                    newER.IsActive = 1;
                }
                else
                {
                    newER.IsActive = 0;
                }

                return exchangeRatesRepository.UpdateExchangeRate(er, newER);
            }
        }

        public void delete(ExchangeRate er)
        {
            exchangeRatesRepository.DeleteExchangeRate(er);
        }
    }
}
