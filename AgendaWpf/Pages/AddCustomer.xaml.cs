using AgendaWpf.Data;
using AgendaWpf.Models;
using System;
using System.Windows;
using System.Windows.Controls;


namespace AgendaWpf.Pages
{
    /// <summary>
    /// Logique d'interaction pour AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {
        private readonly AgendaContext _db = new();
        public AddCustomer()
        {   
            InitializeComponent();
        }


        // Ajout client dans bdd
        private void AddNewCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer custom = new()
                {
                    Firstname = customerFirstname.Text,
                    Lastname = customerLastname.Text,
                    PhoneNumber = customerPhone.Text,
                    Mail = customerEmail.Text,
                    Budget = int.Parse(customerBudget.Text),
                };
                _db.Add(custom);
                _db.SaveChanges();
                MessageBox.Show("New customer created");
            }
            catch (Exception)
            {
                MessageBox.Show("Customer can't be created");
                customerFirstname.Clear();
                customerLastname.Clear();
                customerBudget.Clear();
                customerEmail.Clear();
                customerPhone.Clear();
            }
        }


        //Annuler l'ajout
        private void CancelAddNewCustomer(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
