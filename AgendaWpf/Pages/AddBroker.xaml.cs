using AgendaWpf.Data;
using AgendaWpf.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AgendaWpf.Pages
{
    /// <summary>
    /// Logique d'interaction pour AddBroker.xaml
    /// </summary>
    public partial class AddBroker : Page
    {
        private readonly AgendaContext _db = new();
        public AddBroker()
        {
            InitializeComponent();
        }

        // Add new broker to the database
        private void AddNewBroker(object sender, RoutedEventArgs e)
        {
            try
            {
                Broker brok = new()
                {
                    Firstname = brokerFirstname.Text,
                    Lastname = brokerLastname.Text,
                    PhoneNumber = brokerPhone.Text,
                    Mail = brokerEmail.Text,
                };
                _db.Add(brok);
                _db.SaveChanges();
                MessageBox.Show("New broker created");
            }
            catch (Exception)
            {
                MessageBox.Show("Broker can't be created");
                brokerLastname.Clear();
                brokerPhone.Clear();
                brokerFirstname.Clear();
                brokerEmail.Clear();
            }
        }

        public void CancelAddNewBroker(object sender, RoutedEventArgs e) { 
            this.NavigationService.GoBack();
        }
    }
}
