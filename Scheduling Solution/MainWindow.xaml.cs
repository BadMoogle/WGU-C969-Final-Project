using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using DBLogic;

namespace Scheduling_Solution
{
    //Class only used by MainWindow.  Used to format the display of the datagrid.

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public ObservableCollection<AppointmentDisplay> appointmentList; //Used to manage the data in the AppointmentList Datagrid
        private bool calendarChanged; //Used to prevent stack overflow issues with recursive lookups when changing dates. (Basically selecteddatechanged will overflow
                                     //the stack from calling itself)

        public MainWindow()
        {
            InitializeComponent();
            appointmentList = new ObservableCollection<AppointmentDisplay>();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            calendarChanged = false;
            //Bind the list of system timezones to the combobox and set the current one to default
            cmbxTimezone.DataContext = TimeZoneInfo.GetSystemTimeZones();
            cmbxTimezone.SelectedValue = TimeZoneInfo.Local;
            //Enable and check DaylightSavingsTime if it's available to the local timezone
            chxbxDaylightSavings.IsChecked = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now) ? true : false;
            PopulateAppointmentsForDate(DateTime.UtcNow);
            foreach (Appointment a in Globals.Appointments.Where(d => (d.Start >= DateTime.UtcNow && d.Start <= DateTime.UtcNow.AddMinutes(15)))) //filter list of appointments to check those within 15 minutes
            {
                MessageBox.Show("Appointment with " + a.AssociatedCustomer.CustomerName + " is within the next 15 minutes.\n\nTitle: " + a.Title +
                    "\nContact: " + a.Contact + "\nLocation: " +
                    a.Location + "\nDescription: " + a.Description, "Appointment Reminder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            cldCalendar.SelectedDate = DateTime.Now;
        }

        //Exit menu item is clicked
        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Show the dialog to add a new Appointment
        private void mnuFileAppointments_Click(object sender, RoutedEventArgs e)
        {
            AddEditAppointment addEditAppointment = new AddEditAppointment();
            addEditAppointment.ShowDialog();
            RefreshAppointmentList();
        }

        //Show the dialog to add a new customer
        private void mnuFileCustomers_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomer wndCust = new AddEditCustomer();
            wndCust.ShowDialog();
        }

        //Show the dialog to add a new Appointment
        private void BtnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            AddEditAppointment addEditAppointment = new AddEditAppointment();
            addEditAppointment.ShowDialog();
            RefreshAppointmentList();
        }

        //Enable the buttons when the user selects an appointment in the list
        private void DgrdAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrdAppointments.SelectedItems.Count != 0)
            {
                btnDeleteAppointment.IsEnabled = true;
                btnEditAppointment.IsEnabled = true;
            }
            else
            {
                btnDeleteAppointment.IsEnabled = false;
                btnEditAppointment.IsEnabled = false;
            }
            
        }

        //Delete the selected appointment
        private void BtnDeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdAppointments.SelectedItems.Count != 0)
            {
                AppointmentDisplay appr = (AppointmentDisplay)dgrdAppointments.SelectedItem;
                Appointment appoint = new Appointment(Globals.Appointments.Where(d => d.AppointmentId == appr.AppointmentId).First()); //Take the selection from the datagrid and find the appropriate object in the collection
                Globals.Appointments.Remove(appoint);
                appoint.DeleteFromDatabase();
            }

        }

        //Update the datagrid when the user selects a new date
        private void CldCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAppointmentList();
        }

        //Update the appointment list when the user changes the selection mode
        private void CmbxDateSelectionMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAppointmentList();
        }

        //show the dialog to show the customer list
        private void mnuReportingCustomerList_Click(object sender, RoutedEventArgs e)
        {
            CustomerList customerList = new CustomerList();
            customerList.ShowDialog();
        }

        //Event handler when the user clicks the edit button
        private void BtnEditAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdAppointments.SelectedItems.Count != 0)
            {
                AppointmentDisplay appr = (AppointmentDisplay)dgrdAppointments.SelectedItem;
                AddEditAppointment addEditAppointment = new AddEditAppointment(Globals.Appointments.Where(d => d.AppointmentId == appr.AppointmentId).First()); //Take the selection from the datagrid and find the appropriate object in the collection
                addEditAppointment.ShowDialog();
                RefreshAppointmentList();
            }
        }

        //Event handler when the user changes the time zone combo box
        private void CmbxTimezone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAppointmentList();
        }

        //Event handler for the Appointment Type menu item.
        private void mnuReportingAppointmentType_Click(object sender, RoutedEventArgs e)
        {
            RptAppointmentType rpt = new RptAppointmentType();
            rpt.ShowDialog();
        }

        //Used to refresh the appointment list.  This includes adding or editing appointments, changing the timezones, or some other reason on why the display will need to be refreshed.
        private void RefreshAppointmentList()
        {
            if (calendarChanged == false)
            {
                TimeZoneInfo tz = cmbxTimezone.SelectedValue == null ? TimeZoneInfo.Local : (TimeZoneInfo)cmbxTimezone.SelectedValue; //Safety check
                DateTime date = cldCalendar.SelectedDate.GetValueOrDefault();
                chxbxDaylightSavings.IsChecked = tz.IsDaylightSavingTime(date) ? true : false; //Check the checkbox for DST if the selected date is DST within the selected Timezone
                if (cldCalendar.SelectedDate == null) //Safety check
                {
                    cldCalendar.SelectedDate = DateTime.Today;
                }
                //If the user has daily selected, then update the appointment list just for that day
                if (cmbxDateSelectionMode.SelectedIndex == 0)
                {
                    PopulateAppointmentsForDate(cldCalendar.SelectedDate.Value.ToUniversalTime());
                }
                //If the user has weekly selected, then update the appointment list for everything in that week
                else if (cmbxDateSelectionMode.SelectedIndex == 1)
                {
                    DateTime selected = cldCalendar.SelectedDate.GetValueOrDefault().ToUniversalTime();
                    calendarChanged = true;
                    cldCalendar.SelectedDates.Add(selected);
                    //Update the calendar with the entire week from the day the user selects.  These take the day of the week and then selects the dates based upon the day of the week that was selected.
                    switch (selected.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            for (int i = 1; i < 7; i++)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected, selected.AddDays(7));
                            break;
                        case DayOfWeek.Monday:
                            cldCalendar.SelectedDates.Add(selected.AddDays(-1));
                            for (int i = 1; i < 6; i++)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected.AddDays(-1), selected.AddDays(6));
                            break;
                        case DayOfWeek.Tuesday:
                            for (int i = -1; i > -3; i--)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            for (int i = 1; i < 5; i++)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected.AddDays(-2), selected.AddDays(5));
                            break;
                        case DayOfWeek.Wednesday:
                            for (int i = -1; i > -4; i--)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            for (int i = 1; i < 4; i++)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected.AddDays(-3), selected.AddDays(4));
                            break;
                        case DayOfWeek.Thursday:
                            for (int i = -1; i > -5; i--)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            for (int i = 1; i < 3; i++)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected.AddDays(-4), selected.AddDays(3));
                            break;
                        case DayOfWeek.Friday:
                            for (int i = -1; i > -6; i--)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            for (int i = 1; i < 2; i++)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected.AddDays(-5), selected.AddDays(2));
                            break;
                        case DayOfWeek.Saturday:
                            for (int i = -1; i > -7; i--)
                            {
                                cldCalendar.SelectedDates.Add(selected.AddDays(i));
                            }
                            PopulateAppointmentsForDateRange(selected.AddDays(-6), selected);
                            break;
                    };
                    calendarChanged = false;
                }
                //If the user has monthly selected, then update the appointment list
                else if (cmbxDateSelectionMode.SelectedIndex == 2)
                {
                    int year = cldCalendar.SelectedDate.Value.Year;
                    int month = cldCalendar.SelectedDate.Value.Month;
                    calendarChanged = true;
                    cldCalendar.SelectedDates.AddRange(new DateTime(year, month, 1), new DateTime(year, month, DateTime.DaysInMonth(year, month)));
                    PopulateAppointmentsForDateRange(new DateTime(year, month, 1), new DateTime(year, month, DateTime.DaysInMonth(year, month)));
                    calendarChanged = false;
                }
            }
        }

        //Used to update the collection for when a single date is selected
        private void PopulateAppointmentsForDate(DateTime date)
        {
            TimeZoneInfo tz = cmbxTimezone.SelectedValue == null ? TimeZoneInfo.Local : (TimeZoneInfo)cmbxTimezone.SelectedValue;
            if (appointmentList == null)  //safety check
            {
                appointmentList = new ObservableCollection<AppointmentDisplay>();
            }
            //MessageBox.Show(cmbxTimezone.SelectedValue.ToString());
            appointmentList.Clear();
            var apps = from p in Globals.Appointments where TimeZoneInfo.ConvertTime(p.Start, tz).ToString("yyyy-MM-dd") == TimeZoneInfo.ConvertTime(date, tz).ToString("yyyy-MM-dd") select p;//Get our list of appointments that match the date range
            foreach (Appointment a in apps) //for each appointment add it to the list and convert the timezone at the same time.
            {
                appointmentList.Add(new AppointmentDisplay(a.AppointmentId, a.AssociatedCustomer.CustomerName, a.Contact, TimeZoneInfo.ConvertTime(a.Start, tz).ToString("yyyy-MM-dd hh: mm tt"), 
                    TimeZoneInfo.ConvertTimeFromUtc(a.End, tz).ToString("yyyy-MM-dd hh: mm tt")));
            }
            dgrdAppointments.DataContext = appointmentList;
        }

        //Used to update the collection for when a date range is selected
        private void PopulateAppointmentsForDateRange(DateTime start, DateTime end)
        {
            TimeZoneInfo tz = cmbxTimezone.SelectedValue == null ? TimeZoneInfo.Local : (TimeZoneInfo)cmbxTimezone.SelectedValue;
            if (appointmentList == null) //safety check
            {
                appointmentList = new ObservableCollection<AppointmentDisplay>();
            }
            appointmentList.Clear();
            var apps = from p in Globals.Appointments where TimeZoneInfo.ConvertTime(p.Start, tz) >= TimeZoneInfo.ConvertTime(start, tz) && 
                       TimeZoneInfo.ConvertTime(p.Start, tz) <= TimeZoneInfo.ConvertTime(end, tz) select p; //Get our list of appointments that match the date range
            foreach (Appointment a in apps) //for each appointment add it to the list and convert the timezone at the same time.
            {
                appointmentList.Add(new AppointmentDisplay(a.AppointmentId, a.AssociatedCustomer.CustomerName, a.Contact, TimeZoneInfo.ConvertTimeFromUtc(a.Start, tz).ToString("yyyy-MM-dd hh: mm tt"), 
                    TimeZoneInfo.ConvertTimeFromUtc(a.End, tz).ToString("yyyy-MM-dd hh: mm tt")));
            }
            dgrdAppointments.DataContext = appointmentList;
        }

        //Show the dialog for the Contact Schedule report
        private void mnuReportingContactSchedule_Click(object sender, RoutedEventArgs e)
        {
            RptContactSchedule rpt = new RptContactSchedule();
            rpt.ShowDialog();
        }

        //When the user selects the Appointment by Country menu item
        private void mnuReportingAppointmentsByCountry_Click(object sender, RoutedEventArgs e)
        {
            RptAppointmentsByCountry rpt = new RptAppointmentsByCountry();
            rpt.ShowDialog();
        }

        //When the user selects the Number of Appointment Types by month menu item
        private void mnuReportingNumAppointTypeByMonth_Click(object sender, RoutedEventArgs e)
        {
            RptNumAppointTypeByMonth rpt = new RptNumAppointTypeByMonth();
            rpt.ShowDialog();
        }
    }
}
