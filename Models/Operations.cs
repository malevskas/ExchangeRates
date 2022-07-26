using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Models
{
    internal class Operations
    {
        public int OperationId { get; set; }
        public int OperationTypeId { get; set; }
        public OperationTypes OperationType { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        [DataType(DataType.Date)]
        public DateTime OperationDate { get; set; }
        public int Amount { get; set; }
        [ForeignKey("OperationCurrencyFrom")]
        public int OperationCurrencyFrom { get; set; }
        public Currencies CurrenciesFrom { get; set; }
        [ForeignKey("OperationCurrencyTo")]
        public int OperationCurrencyTo { get; set; }
        public Currencies CurrenciesTo { get; set; }
    }
}
