using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Models
{
    internal class Users
    {
        public int UserId { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        public int IsActive { get; set; }
        public ICollection<Operations> Operations { get; set; }
    }
}
