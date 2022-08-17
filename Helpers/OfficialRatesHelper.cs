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
    internal class OfficialRatesHelper
    {
        private readonly IOfficialRatesRepository officialRatesRepository = new OfficialRatesRepository();

        public List<Currency> fillCB()
        {
            return officialRatesRepository.GetAllCurrencies();
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
                Currency c = officialRatesRepository.GetCurrencyByName(name);
                OfficialRate o = new OfficialRate();
                o.Currency = c.CurrencyId;
                o.ValidityDate = zbir.Datum;
                o.Rate = zbir.Sreden;
                o.IsActive = 1;

                if (!officialRatesRepository.EntryExists(name))
                {
                    officialRatesRepository.InsertOfficialRate(o);
                }
            }

            return officialRatesRepository.GetAllOfficialRates();
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

                return officialRatesRepository.InsertOfficialRate(or);
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
                OfficialRate newOR = new OfficialRate();
                newOR.ValidityDate = SelectedDate.Value.Date;
                newOR.Currency = c.CurrencyId;
                newOR.Rate = int.Parse(Rate);
                if (IsChecked == true)
                {
                    newOR.IsActive = 1;
                }
                else
                {
                    newOR.IsActive = 0;
                }

                return officialRatesRepository.UpdateOfficialRate(or, newOR);
            }
        }

        public void delete(OfficialRate or)
        {
            officialRatesRepository.DeleteOfficialRate(or);
        }
    }
}
