using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Models
{
    internal class OfficialRates
    {
        public int OfficialRatesId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidityDate { get; set; }
        public int Currency { get; set; }
        public Currencies Currencies { get; set; }
        public float Rate { get; set; }
        public int IsActive { get; set; }
    }
}
