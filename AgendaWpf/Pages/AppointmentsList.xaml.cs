using AgendaWpf.Data;
using AgendaWpf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AgendaWpf.Pages
{
    /// <summary>
    /// Logique d'interaction pour AppointmentsList.xaml
    /// </summary>
    public partial class AppointmentsList : Page
    {
        private readonly AgendaContext _db = new();
        public AppointmentsList()
        {
            InitializeComponent();
            DisplayAppointments();
        }



        //Afficher rdv
        public void DisplayAppointments()
        {
            var query = _db.Appointments.Include(b => b.IdBrokerNavigation).Include(c => c.IdCustomerNavigation).ToList();
            if (query.Any())
            {
                appointmentlist.ItemsSource = query;
            }
            else
            {
                MessageBox.Show("No appointment found");
            }

       
        }

        //Supprimer rdv
        private void DeleteAppointment(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment? row = appointmentlist.SelectedItem as Appointment;
                var app = (from a in _db.Appointments
                           where a.IdAppointment == row.IdAppointment
                           select a).Single();
                if(app != null)
                {
                    _db.Appointments.Remove(app);
                    _db.SaveChanges();
                    MessageBox.Show("Appointment deleted");
                    this.NavigationService.Refresh();
                }
                else{
                    MessageBox.Show("No appointment to delete");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't delete Appointment");
            }
        }

        private void DisplayEditForm(object sender, RoutedEventArgs e)
        {
            EditAppointmentForm.Visibility = Visibility.Visible;
            var row = appointmentlist.SelectedItem as Appointment;
            var appointment = (from a in _db.Appointments
                         where a.IdAppointment == row.IdAppointment
                         select a).Single();
            var hour = row.DateHour.ToString().Substring(11,2);
            Hour.Text = hour;
            var minutes = row.DateHour.ToString().Substring(14, 2);
            Minutes.Text = minutes;
            day.Text = row.DateHour.ToString();
            appointmentId.Text = row.IdAppointment.ToString();
            appointmentSubject.Text = row.Subject;
            appointmentBroker.ItemsSource = (from b in _db.Brokers select new { b.IdBroker, b.Firstname, b.Lastname }).ToList();
            appointmentCustomer.ItemsSource = (from c in _db.Customers select new { c.IdCustomer, c.Firstname, c.Lastname }).ToList();
        }

        private void EditAppointment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (VerifHours(Hour.Text, Minutes.Text))
                {
                    
                    string formerHour = day.ToString().Substring(11, 8);
                    string newHour = Hour.Text + ":" + Minutes.Text + ":" + "00";
                    string dateformat = day.ToString().Replace(formerHour, newHour);
                    DateTime newdate = Convert.ToDateTime(dateformat);
                    Appointment? app = _db.Appointments.Find(Convert.ToInt32(appointmentId.Text));
                    app.IdAppointment = Convert.ToInt32(appointmentId.Text);
                    app.DateHour = newdate;
                    app.Subject = appointmentSubject.Text;
                    app.IdBroker = Convert.ToInt32(appointmentBroker.SelectedValue.ToString());
                    app.IdCustomer = Convert.ToInt32(appointmentCustomer.SelectedValue.ToString());
                   // Disponibilité horaire pour un broker ou customer
                    if (Availability(app))
                    {
                        _db.Appointments.Update(app);
                        _db.SaveChanges();
                        MessageBox.Show("Appointment edited");
                        this.NavigationService.Refresh();
                    }
                }
            }
            catch (Exception)
            {
                this.NavigationService.Refresh();
            }
        }

        // Verif heures et minutes
        private bool VerifHours(string hours, string minutes)
        {
            int hour = int.Parse(hours);
            int minute = int.Parse(minutes);
            if ((hour < 19 && hour > 8) && (minute <= 59 && minute >= 0))
            {
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
                if ((item.DateHour == app.DateHour) && (app.IdAppointment != item.IdAppointment))
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
                if ((item.DateHour == app.DateHour) && (app.IdAppointment != item.IdAppointment))
                {
                    MessageBox.Show("This broker has already an appointment at this moment");
                    return false;
                }
            }
            return true;
        }
    }
}
