using AgendaWpf.Data;
using AgendaWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AgendaWpf.Pages
{

    /// <summary>
    /// Logique d'interaction pour CustomersList.xaml
    /// </summary>
    public partial class CustomersList : Page
    {

        private readonly AgendaContext _db = new();
        public CustomersList()
        {
            InitializeComponent();
            DisplayCustomers();
        }

        //Display all customers
        private void DisplayCustomers()
        {
            var query = (from c in _db.Customers select c).ToList();
            if (query.Any())
            {
                customerlist.ItemsSource = query;
            }
            else
            {
                MessageBox.Show("No customers found");
            }
        }

        //Update customers
        private void EditCustomer(object sender, RoutedEventArgs e)
        {


            try
            {
                Customer? newCustomer = _db.Customers.Find(Convert.ToInt32(customerId.Text));
                newCustomer.Firstname = customerFirstname.Text;
                newCustomer.Lastname = customerLastname.Text;
                newCustomer.PhoneNumber = customerPhone.Text;
                newCustomer.Mail = customerEmail.Text;
                newCustomer.Budget = Convert.ToInt32(customerBudget.Text);

                _db.Customers.Update(newCustomer);
                _db.SaveChanges();
                this.NavigationService.Refresh();
            //EditMenu.Visibility = Visibility.Collapsed;

            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        // Edit customer screen
        private void DisplayEdit(object sender, RoutedEventArgs e)
        {
            EditMenu.Visibility = Visibility.Visible;
            Customer selectedRow = customerlist.SelectedItem as Customer;
            var customer = (from c in _db.Customers
                            where c.IdCustomer == selectedRow.IdCustomer
                            select c).Single();
            customerId.Text = selectedRow.IdCustomer.ToString();
            customerFirstname.Text = selectedRow.Firstname;
            customerLastname.Text = selectedRow.Lastname;
            customerPhone.Text = selectedRow.PhoneNumber;
            customerEmail.Text = selectedRow.Mail;
            customerBudget.Text = selectedRow.Budget.ToString();
        }


        //Remove customer from database
        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer? row = customerlist.SelectedItem as Customer;
                var customer = (from c in _db.Customers
                                where c.IdCustomer == row.IdCustomer
                                select c).Single();
                DeleteCustomerAppointment(row.IdCustomer);
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                MessageBox.Show("Customer deleted");
                this.NavigationService.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Customer can't be deleted");
            }
        }

        //Delete all appointment for a customer we want to remove
        private void DeleteCustomerAppointment(int idcustom)
        {
            var query = (from a in _db.Appointments
                         where a.IdCustomer == idcustom
                         select a).ToList();
            if (!query.Any())
                return;
            else
            {
                foreach (var item in query)
                    _db.Appointments.Remove(item);
                _db.SaveChanges();
            }
        }
    }
}
