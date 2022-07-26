﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        string Insert(T t);
        string Update(T t);
    }
}
