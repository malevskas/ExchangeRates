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
    internal class ExchangeRatesHelper : IExchangeRatesRepository
    {
        private Entity myExchangeDatabase = new Entity();
        private readonly IRepository<ExchangeRate> erRepo = new Repository<ExchangeRate>();

        public List<Currency> fillCB()
        {
            return GetAllCurrencies();
        }

        public List<ExchangeRate> loadTable()
        {
            return erRepo.GetAll();
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
                    er.Rate = Convert.ToDouble(Rate);
                    if (IsChecked == true)
                    {
                        er.IsActive = 1;
                    }
                    else
                    {
                        er.IsActive = 0;
                    }

                    return erRepo.Insert(er);
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

                return erRepo.Update(er);
            }
        }

        public string delete(ExchangeRate er)
        {
            er.IsActive = 0;
            return erRepo.Update(er);
        }

        public List<Currency> GetAllCurrencies()
        {
            return myExchangeDatabase.Currencies.ToList<Currency>();
        }
    }
}
