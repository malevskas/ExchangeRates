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
        private Entity myExchangeDatabase = new Entity();
        
        public List<OperationType> loadTable()
        {
            List<OperationType> allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            return allMyOperationTypes;
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

                myExchangeDatabase.OperationTypes.Add(ot);
                myExchangeDatabase.SaveChanges();
                return "ok";
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

                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }

        public void delete(OperationType ot)
        {
            ot.isActive = 0;
            myExchangeDatabase.SaveChanges();
        }
    }
}
