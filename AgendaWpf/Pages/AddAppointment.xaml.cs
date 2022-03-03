using AgendaWpf.Data;
using AgendaWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AgendaWpf.Pages
{
    /// <summary>
    /// Logique d'interaction pour AddAppointment.xaml
    /// </summary>


    public partial class AddAppointment : Page
    {
        private readonly AgendaContext _db = new();
        public AddAppointment()
        {
            InitializeComponent();
            AddAppointmentForm();
        }

        // Formulaire création rdv
        private void AddAppointmentForm()
        {
            var query = (from c in _db.Customers
                         select new { c.IdCustomer, c.Firstname, c.Lastname }).ToList();

            var query2 = (from b in _db.Brokers
                          select new { b.IdBroker, b.Firstname, b.Lastname }).ToList();

            if(query.Any() && query2.Any())
            {
                appointmentform.Visibility = Visibility.Visible;
                appointmentCustomer.ItemsSource = query;
                appointmentBroker.ItemsSource = query2;
            }
            else
            {
                MessageBox.Show("No customer or broker for an appointment");
            }
        }

        //Ajout de rendez vous
        private void AddAppointmnent(object sender, RoutedEventArgs e)
        {  
            try
            {
                if (VerifFormat(Hour.Text, Minutes.Text))
                {
                    string newHour = Hour.Text + ":" + Minutes.Text + ":" + "00";
                    string dateformat = day.ToString().Replace("00:00:00", newHour);
                    DateTime newdate = Convert.ToDateTime(dateformat);

                    Appointment app = new()
                    {
                        DateHour = Convert.ToDateTime(dateformat),
                        Subject = appointmentSubject.Text,
                        IdBroker = Convert.ToInt32(appointmentBroker.SelectedValue.ToString()),
                        IdCustomer = Convert.ToInt32(appointmentCustomer.SelectedValue.ToString()),
                    };
                    //Disponibilité horaire pour un broker ou customer
                    if (Availability(app))
                    {
                        _db.Appointments.Add(app);
                        _db.SaveChanges();
                        MessageBox.Show("Appointment created");
                    }
                }
            }
            catch (Exception)
            {
                this.NavigationService.Refresh();
            }
}

        // Verif heures et minutes
        private bool VerifFormat(string hours, string minutes)
        {
            int hour= int.Parse(hours);
            int minute= int.Parse(minutes);
            if((hour<19 && hour > 8) && (minute <=59 && minute >=0)) {
                return true;
            }
            MessageBox.Show("Hour must be between 9:00 and 18:59");
            return false;
        }

        //Disponibilité horaire pour customer ou broker
        private bool Availability(Appointment app)
        {
            //Disponibilité pour client
            var query = (from a in _db.Appointments
                         where a.IdCustomer == app.IdCustomer
                         select a).ToList();
            foreach (var item in query)
            {
                if (item.DateHour == app.DateHour)
                {
                    MessageBox.Show("This customer has already an appointment at this moment");
                    return false;
                }
            }
            //Disponibilité broker
            var query2 = (from a in _db.Appointments
                         where a.IdBroker == app.IdBroker
                         select a).ToList();
            foreach (var item in query)
            {
                if (item.DateHour == app.DateHour)
                {
                    MessageBox.Show("This broker has already an appointment at this moment");
                    return false;
                }
            }
            return true;
        }
    }
}
