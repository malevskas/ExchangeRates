using ExchangeRates.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;

namespace ExchangeRates.Helpers
{
    internal class CurrenciesHelper : ICurrenciesRepository
    {
        private Entity myExchangeDatabase = new Entity();
        private readonly IRepository<Currency> currRepo = new Repository<Currency>();

        public List<Currency> loadTable()
        {
            //myExchangeDatabase.Currencies.RemoveRange(allMyCurrencies);
            //myExchangeDatabase.SaveChanges();

            if(!currRepo.GetAll().Any())
            {
                nbrm.Kurs kurs = new nbrm.Kurs();
                var xmlResult = kurs.GetExchangeRateD(DateTime.Today, DateTime.Today);
                XmlSerializer serializer = new XmlSerializer(typeof(dsKurs));
                dsKurs result;

                using (TextReader reader = new StringReader(xmlResult))
                {
                    result = (dsKurs)serializer.Deserialize(reader);
                }

                foreach (dsKursKursZbir zbir in result.Items)
                {
                    Currency c = new Currency();
                    c.Code = Convert.ToString(zbir.Valuta);
                    c.CurrencyName = zbir.Oznaka;
                    c.IsActive = 1;

                    currRepo.Insert(c);
                }
            }

            return currRepo.GetAll();
        }

        public string insert(string CodeText, string CurrencyNameText, bool? IsChecked)
        {
            var allMyCodes = GetCurrencyCodes();
            var allMyCurrencyNames = GetCurrencyNames();

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

                return currRepo.Insert(currency);
            }
        }

        public string edit(Currency currency, string CodeText, string CurrencyNameText, bool? IsChecked)
        {
            if (CodeText == "" || CurrencyNameText == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
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

                return currRepo.Update(currency);
            }
        }

        public string delete(Currency currency)
        {
            currency.IsActive = 0;
            return currRepo.Update(currency);
        }
        public List<string> GetCurrencyCodes()
        {
            return myExchangeDatabase.Currencies.Select(x => x.Code).ToList();
        }

        public List<string> GetCurrencyNames()
        {
            return myExchangeDatabase.Currencies.Select(x => x.CurrencyName).ToList();
        }
    }
}
