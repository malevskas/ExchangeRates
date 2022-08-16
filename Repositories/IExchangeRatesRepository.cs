﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IExchangeRatesRepository
    {
        List<ExchangeRate> GetAllExchangeRates();
        string InsertExchangeRate(ExchangeRate exchangeRate);
        string UpdateExchangeRate(ExchangeRate exchangeRate);
        void DeleteExchangeRate(ExchangeRate exchangeRate);
    }
}