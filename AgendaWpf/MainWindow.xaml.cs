using AgendaWpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AgendaWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AgendaContext _db = new();
        public MainWindow()
        {
            InitializeComponent();
            DisplayCustomers(); //Display all customers when the app start
        }
/*************************************************************************************************************
 *                                          CUSTOMERS
 * **********************************************************************************************************/

        // Open the AddCustomer page
        private void AddCustomerPage(object sender, RoutedEventArgs e)
        {
           // agendaPage.Source = new Uri("Pages/AddCustomer.xaml", UriKind.Relative); //Fonctionne aussi
            agendaPage.Navigate(new System.Uri("Pages/AddCustomer.xaml", UriKind.Relative));
        }


        // Open the page CustomersList page
        private void CustomerList(object sender, RoutedEventArgs e)
        {
            // agendaPage.Source = new Uri("Pages/CustomersList.xaml", UriKind.Relative);
            agendaPage.Navigate(new System.Uri("Pages/CustomersList.xaml", UriKind.Relative));
        }


        //Display all customers
        private void DisplayCustomers()
        {
            agendaPage.Navigate(new System.Uri("Pages/CustomersList.xaml", UriKind.Relative));
        }



/*************************************************************************************************************
*                                          BROKERS
* ***********************************************************************************************************/



        private void AddBrokerPage(object sender, RoutedEventArgs e)
        {
            agendaPage.Navigate(new System.Uri("Pages/AddBroker.xaml", UriKind.Relative));
        }


        private void BrokersList(object sender, RoutedEventArgs e)
        {
            // agendaPage.Source = new Uri("Pages/CustomersList.xaml", UriKind.Relative);
            agendaPage.Navigate(new System.Uri("Pages/BrokersList.xaml", UriKind.Relative));
        }



/*************************************************************************************************************
*                                       APPOINTMENTS
* ***********************************************************************************************************/

        private void AppointmentsList(object sender, RoutedEventArgs e)
        {
            agendaPage.Navigate(new System.Uri("Pages/AppointmentsList.xaml", UriKind.Relative));
        }

        private void AddAppointmentPage(object sender, RoutedEventArgs e)
        {
            agendaPage.Navigate(new System.Uri("Pages/AddAppointment.xaml", UriKind.Relative));
        }

    }


}

