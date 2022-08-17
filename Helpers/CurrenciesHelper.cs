﻿using ExchangeRates.Repositories;
using ExchangeRates.Repository;
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
    internal class CurrenciesHelper
    {
        private readonly ICurrenciesRepository currenciesRepository = new CurrenciesRepository();

        public List<Currency> loadTable()
        {
            //myExchangeDatabase.Currencies.RemoveRange(allMyCurrencies);
            //myExchangeDatabase.SaveChanges();

            if(!currenciesRepository.GetAllCurrencies().Any())
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

                    currenciesRepository.InsertCurrency(c);
                }
            }

            return currenciesRepository.GetAllCurrencies();
        }

        public string insert(string CodeText, string CurrencyNameText, bool? IsChecked)
        {
            var allMyCodes = currenciesRepository.GetCurrencyCodes();
            var allMyCurrencyNames = currenciesRepository.GetCurrencyNames();

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

                return currenciesRepository.InsertCurrency(currency);
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
                Currency newC = new Currency();
                newC.Code = CodeText;
                newC.CurrencyName = CurrencyNameText;
                if (IsChecked == true)
                {
                    newC.IsActive = 1;
                }
                else
                {
                    newC.IsActive = 0;
                }

                return currenciesRepository.UpdateCurrency(currency, newC);
            }
        }

        public void delete(Currency currency)
        {
            currenciesRepository.DeleteCurrency(currency);
        }
    }
}
