using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace ExchangeRates.Helpers
{
    internal class CurrenciesHelper
    {
        private Entity myExchangeDatabase = new Entity();

        public List<Currency> loadTable()
        {
            //myExchangeDatabase.Currencies.RemoveRange(allMyCurrencies);
            //myExchangeDatabase.SaveChanges();

            if(!myExchangeDatabase.Currencies.Any())
            {
                nbrm.Kurs kurs = new nbrm.Kurs();
                var result = kurs.GetExchangeRate("12.08.2022", "12.08.2022");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                //Debug.Write(result);
                foreach (XmlNode node in doc.SelectNodes("//KursZbir"))
                {
                    string name = node["Oznaka"].InnerText;
                    Currency c = new Currency();
                    c.Code = node["Valuta"].InnerText;
                    c.CurrencyName = name;
                    c.IsActive = 1;

                    myExchangeDatabase.Currencies.Add(c);
                    myExchangeDatabase.SaveChanges(); 
                }
            }

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
