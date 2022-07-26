using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Models
{
    internal class Currencies
    {
        public int CurrencyId { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(20)]
        public string CurrencyName { get; set; }
        public int IsActive { get; set; }
        public ICollection<OfficialRates> OfficialRates { get; set; }
        [InverseProperty("ExchangeCurrencyFrom")]
        public ICollection<ExchangeRates> ExchangeCurrenciesFrom { get; set; }
        [InverseProperty("ExchangeCurrencyTo")]
        public ICollection<ExchangeRates> ExchangeCurrenciesTo { get; set; }
        [InverseProperty("OperationCurrencyFrom")]
        public ICollection<ExchangeRates> OperationCurrenciesFrom { get; set; }
        [InverseProperty("OperationCurrencyTo")]
        public ICollection<ExchangeRates> OperationCurrenciesTo { get; set; }
    }
}
