using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class OperationTypesHelper
    {
        private Entity myExchangeDatabase = new Entity();
        private Label OperationTypeId;
        private TextBox Code;
        private TextBox OperationName;
        private CheckBox checkBox;
        private DataGrid dataGrid;

        public OperationTypesHelper(Label OperationTypeId, TextBox Code, TextBox OperationName, CheckBox checkBox, DataGrid dataGrid)
        {
            this.OperationTypeId = OperationTypeId;
            this.Code = Code;
            this.OperationName = OperationName;
            this.checkBox = checkBox;
            this.dataGrid = dataGrid;
        }
        public void loadTable()
        {
            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            dataGrid.ItemsSource = allMyOperationTypes;
        }

        public void insert()
        {
            OperationType ot = new OperationType();
            ot.Code = Code.Text;
            ot.OperationName = OperationName.Text;
            if (checkBox.IsChecked == true)
            {
                ot.isActive = 1;
            }
            else
            {
                ot.isActive = 0;
            }

            myExchangeDatabase.OperationTypes.Add(ot);
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void edit()
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            ot.Code = Code.Text;
            ot.OperationName = OperationName.Text;
            if (checkBox.IsChecked == true)
            {
                ot.isActive = 1;
            }
            else
            {
                ot.isActive = 0;
            }

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void delete()
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            ot.isActive = 0;
            checkBox.IsChecked = false;

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void populateTextBox()
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            OperationTypeId.Content = ot.OperationTypeId.ToString();
            Code.Text = ot.Code;
            OperationName.Text = ot.OperationName;
            if (ot.isActive == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
    }
}
