using AgendaWpf.Data;
using AgendaWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AgendaWpf.Pages
{
    /// <summary>
    /// Logique d'interaction pour BrokersList.xaml
    /// </summary>
    public partial class BrokersList : Page
    {
        private readonly AgendaContext _db = new();
        public BrokersList()
        {
            InitializeComponent();
            DisplayBrokers();
        }


        //Affichage courtier
        public void DisplayBrokers()
        {
            var query = (from b in _db.Brokers select b).ToList();
            if (query.Any())
            {
                brokerslist.ItemsSource = query;
            }
            else
            {
                MessageBox.Show("No brokers found");
            }
        }

        public void EditBroker(object sender, RoutedEventArgs e)
        {
            try
            {
                Broker? newBroker = _db.Brokers.Find(Convert.ToInt32(brokerId.Text));
                newBroker.Firstname = brokerFirstname.Text;
                newBroker.Lastname = brokerLastname.Text;
                newBroker.PhoneNumber = brokerPhone.Text;
                newBroker.Mail = brokerEmail.Text;
                _db.Brokers.Update(newBroker);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        //Affichage formulaire modifiaction courtier
        private void DisplayEdit(object sender, RoutedEventArgs e)
        {
            EditMenu.Visibility = Visibility.Visible;
            Broker? selectedRow = brokerslist.SelectedItem as Broker;
            var broker = (from c in _db.Brokers
                            where c.IdBroker == selectedRow.IdBroker
                            select c).Single();
            brokerId.Text = selectedRow.IdBroker.ToString();
            brokerFirstname.Text = selectedRow.Firstname;
            brokerLastname.Text = selectedRow.Lastname;
            brokerPhone.Text = selectedRow.PhoneNumber;
            brokerEmail.Text = selectedRow.Mail;
        }


        // Delete broker from database
        private void DeleteBroker(object sender, RoutedEventArgs e)
        {
            try
            {
                Broker? row = brokerslist.SelectedItem as Broker;
                var brok = (from b in _db.Brokers
                            where b.IdBroker == row.IdBroker
                            select b).Single();
                DeleteBrokerAppointment(row.IdBroker);
                _db.Brokers.Remove(brok);
                _db.SaveChanges();
                this.NavigationService.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Broker can't be deleted");
            }
        }

        //Supprime tous les rdv d'un courtier
        private void DeleteBrokerAppointment(int idbrok)
        {
            var query = (from a in _db.Appointments
                         where a.IdBroker == idbrok
                         select a).ToList();
            if (query.Any())
            {
                return;
            }
            else
            {
                foreach (var item in query)
                    _db.Appointments.Remove(item);
                _db.SaveChanges();
            }
        }
    }
}
