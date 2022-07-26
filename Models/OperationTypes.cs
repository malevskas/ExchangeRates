using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Models
{
    internal class OperationTypes
    {
        public int OperationTypeId { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(20)]
        public string OperationName { get; set; }
        public int IsActive { get; set; }
    }
}
