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
    internal class OfficialRatesHelper
    {
        private Entity myExchangeDatabase = new Entity();

        public List<Currency> fillCB()
        {
            List<Currency> allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            return allMyCurrencies;
        }

        public List<OfficialRate> loadTable()
        {
            //myExchangeDatabase.OfficialRates.RemoveRange(allMyOfficialRates);
            //myExchangeDatabase.SaveChanges();

            nbrm.Kurs kurs = new nbrm.Kurs();
            var result = kurs.GetExchangeRateD(DateTime.Today, DateTime.Today);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            //Debug.Write(result);
            foreach (XmlNode node in doc.SelectNodes("//KursZbir"))
            {
                string name = node["Oznaka"].InnerText;
                Currency c = myExchangeDatabase.Currencies.Where(cu => cu.CurrencyName == name).FirstOrDefault();
                OfficialRate o = new OfficialRate();
                o.Currency = c.CurrencyId;
                o.ValidityDate = Convert.ToDateTime(node["Datum"].InnerText);
                o.Rate = Convert.ToDouble(node["Sreden"].InnerText);
                o.IsActive = 1;

                if(!myExchangeDatabase.OfficialRates.Contains(o))
                {
                    myExchangeDatabase.OfficialRates.Add(o);
                    myExchangeDatabase.SaveChanges();
                }
            }

            List<OfficialRate> allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();
            return allMyOfficialRates;
        }

        public string insert(DatePicker ValidityDate, /*DateTime? SelectedDate,*/ Currency c, string Rate, bool? IsChecked)
        {
            //var allMyCurrencies = myExchangeDatabase.OfficialRates.Select(x => x.Currency1);
            //if (/*SelectedDate == null || */ValidityDate.SelectedDate.Value.Date == null || c == null || Rate == "")
            //{
            //    return "Please fill out all fields.";
            //}
            //return "";
            ///*else if (DateTime.Compare(SelectedDate.Value.Date, DateTime.Today) < 0)
            //{
            //    return "Please select a later date.";
            //}
            //else
            //{
            //    OfficialRate or = new OfficialRate();
            //    or.ValidityDate = SelectedDate.Value.Date;
            //    or.Currency = c.CurrencyId;
            //    or.Rate = int.Parse(Rate);
            //    if (IsChecked == true)
            //    {
            //        or.IsActive = 1;
            //    }
            //    else
            //    {
            //        or.IsActive = 0;
            //    }

            //    myExchangeDatabase.OfficialRates.Add(or);
            //    myExchangeDatabase.SaveChanges();
            //    return "ok";
            //}*/
            return "ok";
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

                myExchangeDatabase.OfficialRates.Add(or);
                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }

        public void delete(OfficialRate or)
        {
            or.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }
    }
}
