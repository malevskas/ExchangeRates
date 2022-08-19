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
    internal class OfficialRatesHelper : IOfficialRatesRepository
    {
        private Entity myExchangeDatabase = new Entity();
        private readonly IRepository<OfficialRate> orRepo = new Repository<OfficialRate>();

        public List<Currency> fillCB()
        {
            return GetAllCurrencies();
        }

        public List<OfficialRate> loadTable()
        {
            //List<OfficialRate> allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();

            //myExchangeDatabase.OfficialRates.RemoveRange(allMyOfficialRates);
            //myExchangeDatabase.SaveChanges();

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
                string name = zbir.Oznaka;
                Currency c = GetCurrencyByName(name);
                OfficialRate o = new OfficialRate();
                o.Currency = c.CurrencyId;
                o.ValidityDate = zbir.Datum;
                o.Rate = zbir.Sreden;
                o.IsActive = 1;

                if (!EntryExists(name))
                {
                    orRepo.Insert(o);
                }
            }

            return orRepo.GetAll();
        }

        public string insert(DateTime? SelectedDate, Currency c, string Rate, bool? IsChecked)
        {
            if (SelectedDate == null || c == null || Rate == "")
            {
                return "Please fill out all fields.";
            }
            else if (DateTime.Compare(SelectedDate.Value.Date, DateTime.Today) < 0)
            {
                return "Please select a later date.";
            }
            else
            {
                OfficialRate or = new OfficialRate();
                or.ValidityDate = SelectedDate.Value.Date;
                or.Currency = c.CurrencyId;
                or.Rate = int.Parse(Rate);
                if (IsChecked == true)
                {
                    or.IsActive = 1;
                }
                else
                {
                    or.IsActive = 0;
                }

                return orRepo.Insert(or);
            }
        }

        public string edit(OfficialRate or, DateTime? SelectedDate, Currency c, string Rate, bool? IsChecked)
        {
            if (SelectedDate.Value.Date == null || SelectedDate.Value.Date == null || c == null || Rate == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                or.ValidityDate = SelectedDate.Value.Date;
                or.Currency = c.CurrencyId;
                or.Rate = int.Parse(Rate);
                if (IsChecked == true)
                {
                    or.IsActive = 1;
                }
                else
                {
                    or.IsActive = 0;
                }

                return orRepo.Update(or);
            }
        }

        public string delete(OfficialRate or)
        {
            or.IsActive = 0;
            return orRepo.Update(or);
        }

        public List<Currency> GetAllCurrencies()
        {
            return myExchangeDatabase.Currencies.ToList<Currency>();
        }

        public Currency GetCurrencyByName(string name)
        {
            return myExchangeDatabase.Currencies.Where(cu => cu.CurrencyName == name).FirstOrDefault();
        }

        public bool EntryExists(string name)
        {
            return myExchangeDatabase.OfficialRates.Where(or => or.ValidityDate == DateTime.Today && or.Currency1.CurrencyName == name).Any();
        }
    }
}
