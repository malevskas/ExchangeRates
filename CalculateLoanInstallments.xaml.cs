﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for CalculateLoanInstallments.xaml
    /// </summary>
    public partial class CalculateLoanInstallments : Page
    {
        public CalculateLoanInstallments()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            using (var context = new ExchangeRatesDBEntities1())
            {
                var result = context.DailyLoanInstallments(Convert.ToDouble(Amount.Text), Convert.ToInt32(NumOfInstallments.Text), Convert.ToDouble(InterestRate.Text), StartDate.SelectedDate.Value.Day, StartDate.SelectedDate.Value.Month, StartDate.SelectedDate.Value.Year);
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
    }
}
