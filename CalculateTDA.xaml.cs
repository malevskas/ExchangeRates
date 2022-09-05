using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for CalculateTDA.xaml
    /// </summary>
    public partial class CalculateTDA : System.Windows.Controls.Page
    {
        public CalculateTDA()
        {
            InitializeComponent();
        }
        ExchangeRates.CalculateDailyTDA_Result result1;
        private void Calculate(object sender, RoutedEventArgs e)
        {
            using (var context = new ExchangeRatesDBEntities1())
            {
                var result = context.CalculateDailyTDA(Convert.ToDouble(Amount.Text),
                                                       Convert.ToDouble(InterestRate.Text),
                                                       Convert.ToInt32(Period.Text),
                                                       StartDate.SelectedDate.Value.Day,
                                                       StartDate.SelectedDate.Value.Month,
                                                       StartDate.SelectedDate.Value.Year);
                dataGrid.ItemsSource = result;
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void PreviewTextNumericInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Excel(object sender, RoutedEventArgs e)
        {
            try
            {
                var d = dataGrid.ItemsSource.Cast<ExchangeRates.CalculateDailyTDA_Result>();
                var data = ToDataTable(d.ToList());
                ToExcelFile(data, "test.xlsx");
            }
            catch (Exception ex)
            {
                
            }

            //try
            //{
            //    Microsoft.Office.Interop.Excel.Application excel = null;
            //    Microsoft.Office.Interop.Excel.Workbook wb = null;
            //    object missing = Type.Missing;
            //    Microsoft.Office.Interop.Excel.Worksheet ws = null;
            //    Microsoft.Office.Interop.Excel.Range rng = null;

            //    // collection of DataGrid Items
            //    var dtExcelDataTable = ExcelTimeReport(txtFrmDte.Text, txtToDte.Text, strCondition);

            //    excel = new Microsoft.Office.Interop.Excel.Application();
            //    wb = excel.Workbooks.Add();
            //    ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            //    ws.Columns.AutoFit();
            //    ws.Columns.EntireColumn.ColumnWidth = 25;

            //    // Header row
            //    for (int Idx = 0; Idx < dtExcelDataTable.Columns.Count; Idx++)
            //    {
            //        ws.Range["A1"].Offset[0, Idx].Value = dtExcelDataTable.Columns[Idx].ColumnName;
            //    }

            //    // Data Rows
            //    for (int Idx = 0; Idx < dtExcelDataTable.Rows.Count; Idx++)
            //    {
            //        ws.Range["A2"].Offset[Idx].Resize[1, dtExcelDataTable.Columns.Count].Value = dtExcelDataTable.Rows[Idx].ItemArray;
            //    }

            //    excel.Visible = true;
            //    wb.Activate();
            //    //using (System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog() { Filter = "Excel Workbook|*.xlxs" })
            //    //{
            //    //    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    //    {
            //    //        try
            //    //        {
            //    //            using (XLWorkbook workbook = new XLWorkbook())
            //    //            {
            //    //                DataTable dt = new DataTable();
            //    //                dt = (DataTable)dataGrid.ItemsSource.;
            //    //                workbook.Worksheets.Add(dt, "TDA Calculations");
            //    //                workbook.SaveAs(sfd.FileName);
            //    //                workbook.Worksheets.Add();
            //    //            }
            //    //            MessageBox.Show("Success!");
            //    //        }
            //    //        catch (Exception ex)
            //    //        {
            //    //            MessageBox.Show(ex.Message);
            //    //        }
            //    //    }
            //    //}
            //    ////PdfPTable pdfTable = new PdfPTable(dataGrid.Columns.Count);
            //    //foreach(System.Data.DataRowView gvr in dataGrid.ItemsSource)
            //    //{
            //    //    foreach(TableCell tableCell in gvr.Cells)
            //    //    {
            //    //        Debug.Write(tableCell);
            //    //    }
            //}

            //catch
            //{

            //}
        }


        public static System.Data.DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new System.Data.DataTable(typeof(T).Name);

            //Get all the properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    //inserting property values to data table rows
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check data table
            return dataTable;
        }

        public static void ToExcelFile(System.Data.DataTable dataTable, string filePath, bool overwriteFile = true)
        {
            if (File.Exists(filePath) && overwriteFile)
                File.Delete(filePath);

            using (var connection = new OleDbConnection())
            {
                connection.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};" +
                                              "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    var columnNames = (from DataColumn dataColumn in dataTable.Columns select dataColumn.ColumnName).ToList();
                    var tableName = !string.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TABLE [{tableName}] ({string.Join(",", columnNames.Select(c => $"[{c}] VARCHAR").ToArray())});";
                    command.ExecuteNonQuery();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var rowValues = (from DataColumn column in dataTable.Columns select (row[column] != null && row[column] != DBNull.Value) ? row[column].ToString() : string.Empty).ToList();
                        command.CommandText = $"INSERT INTO [{tableName}]({string.Join(",", columnNames.Select(c => $"[{c}]"))}) VALUES ({string.Join(",", rowValues.Select(r => $"'{r}'").ToArray())});";
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }
    }
    }

