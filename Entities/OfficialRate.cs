namespace ExchangeRates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OfficialRate
    {
        [Key]
        public int OfficialRatesId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidityDate { get; set; }

        public int? Currency { get; set; }

        public double? Rate { get; set; }

        public int? IsActive { get; set; }

        public virtual Currency Currency1 { get; set; }
    }
}
