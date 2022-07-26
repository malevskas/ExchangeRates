using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Models
{
    internal class ExchangeRates
    {
        public int ExchangeRatesId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidityDate { get; set; }
        [ForeignKey("ExchangeCurrencyFrom")]
        public int ExchangeCurrencyFrom { get; set; }
        public Currencies ExchangeCurrenciesFrom { get; set; }
        [ForeignKey("ExchangeCurrencyTo")]
        public int ExchangeCurrencyTo { get; set; }
        public Currencies ExchangeCurrenciesTo { get; set; }
        public float Rate { get; set; }
        public int IsActive { get; set; }
    }
}
