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
        private readonly IOperationTypesRepository operationTypesRepository = new OperationTypesRepository();

        public List<OperationType> loadTable()
        {
            return operationTypesRepository.GetAllOperationTypes();
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

                return operationTypesRepository.InsertOperationType(ot);
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
                OperationType newOT = new OperationType();
                newOT.Code = Code;
                newOT.OperationName = OperationName;
                if (IsChecked == true)
                {
                    newOT.isActive = 1;
                }
                else
                {
                    newOT.isActive = 0;
                }

                return operationTypesRepository.UpdateOperationType(ot, newOT);
            }
        }

        public void delete(OperationType ot)
        {
            operationTypesRepository.DeleteOperationType(ot);
        }
    }
}
