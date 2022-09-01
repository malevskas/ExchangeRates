using ExchangeRates.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using System.Configuration;
using System.Windows.Shapes;
using System.Net.Http.Headers;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private UsersHelper uHelper;
        private readonly string url = ConfigurationManager.AppSettings["baseUrl"];

        public UsersPage()
        {
            InitializeComponent();
            uHelper = new UsersHelper();
        }

        private async void LoadTable(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.GetToken());
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJTaW1vbmFNYWxldnNrYSIsIm5iZiI6MTY2MTQxNzMzMiwiZXhwIjoxNjYxNDE5MTMyLCJpYXQiOjE2NjE0MTczMzJ9.FN458S_4FawiSaHMyOWpPnDzbP_47CB93YkLKo0Yatc");
                
                HttpResponseMessage response = await client.GetAsync(url + "Users");
                List<User> list = await response.Content.ReadAsAsync<List<User>>();
                dataGrid.ItemsSource = list;
            }
        }
        
        private async void Insert(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.FirstName = FirstName.Text;
            user.Surname = Surname.Text;
            if (checkBox.IsChecked == true)
            {
                user.IsActive = 1;
            }
            else
            {
                user.IsActive = 0;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.GetToken());

                    HttpResponseMessage response = await client.PostAsJsonAsync(url + "Users", user);
                    response.EnsureSuccessStatusCode();
                    string msg = await response.Content.ReadAsStringAsync();
                    response = await client.GetAsync(url + "Users");
                    List<User> list = await response.Content.ReadAsAsync<List<User>>();
                    dataGrid.ItemsSource = list;
                } catch
                {
                    MessageBox.Show("Failed!");
                }
            }

            //string result = uHelper.insert(FirstName.Text, Surname.Text, checkBox.IsChecked);
            //if (!result.Equals("ok"))
            //{
            //    MessageBox.Show(result);
            //}
            //else
            //{
            //dataGrid.ItemsSource = uHelper.loadTable();
            //}
        }

        private async void Edit(object sender, RoutedEventArgs e)
        {
            User user = (User)dataGrid.SelectedItem;
            user.FirstName = FirstName.Text;
            user.Surname = Surname.Text;
            if (checkBox.IsChecked == true)
            {
                user.IsActive = 1;
            }
            else
            {
                user.IsActive = 0;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.GetToken());

                    HttpResponseMessage response = await client.PutAsJsonAsync(url + "Users/" + user.UserId.ToString(), user);
                    response.EnsureSuccessStatusCode();
                    string msg = await response.Content.ReadAsStringAsync();
                    response = await client.GetAsync(url + "Users");
                    List<User> list = await response.Content.ReadAsAsync<List<User>>();
                    dataGrid.ItemsSource = list;
                }
                catch
                {
                    MessageBox.Show("Failed!");
                }
            }

            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        HttpResponseMessage response = await client.PutAsJsonAsync(url + "Users/" + user.UserId.ToString(), user);
            //        response.EnsureSuccessStatusCode();
            //        string msg = await response.Content.ReadAsStringAsync();
            //        response = await client.GetAsync(url + "Users");
            //        List<User> list = await response.Content.ReadAsAsync<List<User>>();
            //        dataGrid.ItemsSource = list;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Failed!");
            //    }
            //}


            //string result = uHelper.edit((User)dataGrid.SelectedItem, FirstName.Text, Surname.Text, checkBox.IsChecked);
            //if (!result.Equals("ok"))
            //{
            //    MessageBox.Show(result);
            //}
            //else
            //{
            //    dataGrid.ItemsSource = uHelper.loadTable();
            //}
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            User user = (User)dataGrid.SelectedItem;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.GetToken());

                    HttpResponseMessage response = await client.PutAsJsonAsync(url + "Users/" + user.UserId.ToString(), user);
                    response.EnsureSuccessStatusCode();
                    string msg = await response.Content.ReadAsStringAsync();
                    response = await client.GetAsync(url + "Users");
                    List<User> list = await response.Content.ReadAsAsync<List<User>>();
                    dataGrid.ItemsSource = list;
                }
                catch
                {
                    MessageBox.Show("Failed!");
                }
            }

            string result = uHelper.delete((User)dataGrid.SelectedItem);
            if (result != "ok")
            {
                MessageBox.Show(result);
            }
            else
            {
                checkBox.IsChecked = false;
                dataGrid.ItemsSource = uHelper.loadTable();
            }
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            User user = (User)dataGrid.SelectedItem;
            if(user != null)
            {
                UserId.Content = user.UserId.ToString();
                FirstName.Text = user.FirstName;
                Surname.Text = user.Surname;
                if (user.IsActive == 1)
                {
                    checkBox.IsChecked = true;
                }
                else
                {
                    checkBox.IsChecked = false;
                }
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z ]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
