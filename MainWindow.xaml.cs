using ExchangeRates.Entities;
using ExchangeRates.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly string url = ConfigurationManager.AppSettings["baseUrl"];

        public MainWindow()
        {
            InitializeComponent();
            Currencies.Visibility = Visibility.Collapsed;
            ExchangeRates.Visibility = Visibility.Collapsed;
            OfficialRates.Visibility = Visibility.Collapsed;
            Operations.Visibility = Visibility.Collapsed;
            OperationTypes.Visibility = Visibility.Collapsed;
            Users.Visibility = Visibility.Collapsed;
            CalculateTDA.Visibility = Visibility.Collapsed;
            CalculateLoanInstallments.Visibility = Visibility.Collapsed;
        }

        private void CurrenciesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CurrenciesPage();
        }
        private void ExchangeRatesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ExchangeRatesPage();
        }
        private void OfficialRatesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OfficialRatesPage();
        }
        private void OperationsMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OperationsPage();
        }
        private void OperationTypesMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OperationTypesPage();
        }
        private void UsersMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UsersPage();
        }

        private void CalculateTDAMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CalculateTDA();
        }

        private void CalculateInstallmentsMenu(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CalculateLoanInstallments();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var auth = new Auth();
            auth.UserName = UserName.Text;
            auth.Password = Password.Text;
            //var json = JsonConvert.SerializeObject(auth);
            using (HttpClient client = new HttpClient())
            {
                //var request = new HttpRequestMessage
                //{
                //    Method = HttpMethod.Post,
                //    RequestUri = new Uri(url + "Token/Authenticate"),
                //    Content = new StringContent(json, Encoding.UTF8, "application/json"),
                //};

                //var content = new FormUrlEncodedContent(json);

                var response = await client.PostAsJsonAsync(url + "Token/Authenticate", auth);

                var responseString = await response.Content.ReadAsStringAsync();

                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    Currencies.Visibility = Visibility.Visible;
                    ExchangeRates.Visibility = Visibility.Visible;
                    OfficialRates.Visibility = Visibility.Visible;
                    Operations.Visibility = Visibility.Visible;
                    OperationTypes.Visibility = Visibility.Visible;
                    Users.Visibility = Visibility.Visible;
                    CalculateTDA.Visibility = Visibility.Visible;
                    CalculateLoanInstallments.Visibility = Visibility.Visible;

                    usern.Visibility = Visibility.Collapsed;
                    UserName.Visibility = Visibility.Collapsed;
                    Pass.Visibility = Visibility.Collapsed;
                    Password.Visibility = Visibility.Collapsed;
                    but.Visibility = Visibility.Collapsed;

                    Token.SetToken(responseString);
                }
                else if (statusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("User unauthorized!");
                }

                //client.SendAsync(request).Wait();

                //var response = (HttpWebResponse)request.GetResponse();

                //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //HttpResponseMessage response = await client.PostAsJsonAsync(url + "Token/Authenticate", FirstName.Text+Surname1.Text, Password.Text);


                //var response = await client.SendAsync(request).ConfigureAwait(false);
                //var responseInfo = await response.Content.ReadAsStringAsync();

                //var response = client.SendAsync(request).ConfigureAwait(false);
                ////var response = await client.SendAsync(request).ConfigureAwait(false);
                ////response.EnsureSuccessStatusCode();
                //var responseInfo = response.GetAwaiter().GetResult();
                ////var responseBody = await response.Content.ReadAsStringAsync();


                //HttpResponseMessage response = await client.PostAsJsonAsync(url + "Token/Authenticate", FirstName.Text+Surname1.Text, Password.Text);
                //List<User> list = await response.Content.ReadAsAsync<List<User>>();
            }
        }
    }
}
