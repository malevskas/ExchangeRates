using ExchangeRates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class OperationTypesHelper
    {
        //private readonly IOperationTypesRepository operationTypesRepository = new OperationTypesRepository();
        private readonly IRepository<OperationType> otRepo = new Repository<OperationType>();

        public List<OperationType> loadTable()
        {
            return otRepo.GetAll();
        }

        public string insert(string Code, string OperationName, bool? IsChecked)
        {
            if (Code == ""  || OperationName == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                OperationType ot = new OperationType();
                ot.Code = Code;
                ot.OperationName = OperationName;
                if (IsChecked == true)
                {
                    ot.isActive = 1;
                }
                else
                {
                    ot.isActive = 0;
                }

                return otRepo.Insert(ot);
            }
        }

        public string edit(OperationType ot, string Code, string OperationName, bool? IsChecked)
        {
            if (Code == "" || OperationName == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                ot.Code = Code;
                ot.OperationName = OperationName;
                if (IsChecked == true)
                {
                    ot.isActive = 1;
                }
                else
                {
                    ot.isActive = 0;
                }

                return otRepo.Update(ot);
            }
        }

        public void delete(OperationType ot)
        {
            ot.isActive = 0;
            otRepo.Update(ot);
        }
    }
}
