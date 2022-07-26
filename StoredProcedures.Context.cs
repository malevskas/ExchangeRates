﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExchangeRates
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ExchangeRatesDBEntities1 : DbContext
    {
        public ExchangeRatesDBEntities1()
            : base("name=ExchangeRatesDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<CalculateDailyTDA_Result> CalculateDailyTDA(Nullable<double> amount, Nullable<double> interestRate, Nullable<int> period, Nullable<int> day, Nullable<int> month, Nullable<int> year)
        {
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(double));
    
            var interestRateParameter = interestRate.HasValue ?
                new ObjectParameter("InterestRate", interestRate) :
                new ObjectParameter("InterestRate", typeof(double));
    
            var periodParameter = period.HasValue ?
                new ObjectParameter("Period", period) :
                new ObjectParameter("Period", typeof(int));
    
            var dayParameter = day.HasValue ?
                new ObjectParameter("Day", day) :
                new ObjectParameter("Day", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CalculateDailyTDA_Result>("CalculateDailyTDA", amountParameter, interestRateParameter, periodParameter, dayParameter, monthParameter, yearParameter);
        }
    
        public virtual ObjectResult<DailyLoanInstallments_Result> DailyLoanInstallments(Nullable<double> amount, Nullable<int> numOfInstallments, Nullable<double> interestRate, Nullable<int> day, Nullable<int> month, Nullable<int> year)
        {
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(double));
    
            var numOfInstallmentsParameter = numOfInstallments.HasValue ?
                new ObjectParameter("NumOfInstallments", numOfInstallments) :
                new ObjectParameter("NumOfInstallments", typeof(int));
    
            var interestRateParameter = interestRate.HasValue ?
                new ObjectParameter("InterestRate", interestRate) :
                new ObjectParameter("InterestRate", typeof(double));
    
            var dayParameter = day.HasValue ?
                new ObjectParameter("Day", day) :
                new ObjectParameter("Day", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DailyLoanInstallments_Result>("DailyLoanInstallments", amountParameter, numOfInstallmentsParameter, interestRateParameter, dayParameter, monthParameter, yearParameter);
        }
    }
}
