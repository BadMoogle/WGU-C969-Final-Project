using System;
using System.Windows;
using DBLogic;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for AddEditAppointment.xaml
    /// </summary>
    public partial class AddEditAppointment : Window
    {
        private Appointment appointment;
        private bool IsNewAppointment { get; set; }

        public AddEditAppointment()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            //Bind the list of system timezones to the combobox and set the current one to default
            cmbbxTimeZone.DataContext = TimeZoneInfo.GetSystemTimeZones();
            cmbbxTimeZone.SelectedValue = TimeZoneInfo.Local;
            cmbxAppointmentType.ItemsSource = Globals.AppointmentTypes;
            dgrdCustomerList.ItemsSource = Globals.Customers;
            //Populate the combo boxes
            DateTime time = new DateTime(2019, 01, 01, 00, 00, 00);
            while(time.ToString("dd") != "02")
            {
                cmbbxStartTime.Items.Add(time.ToString("hh:mm tt"));
                cmbbxEndTime.Items.Add(time.ToString("hh:mm tt"));
                time = time.AddMinutes(15);
            }
            appointment = new Appointment();
            IsNewAppointment = true;
        }

        public AddEditAppointment(Appointment appoint)
        {
            InitializeComponent();
            //Bind the list of system timezones to the combobox and set the current one to default
            cmbbxTimeZone.DataContext = TimeZoneInfo.GetSystemTimeZones();
            cmbbxTimeZone.SelectedValue = TimeZoneInfo.Local;
            dgrdCustomerList.ItemsSource = Globals.Customers;
            cmbxAppointmentType.ItemsSource = Globals.AppointmentTypes;
            //Populate the combo boxes
            DateTime time = new DateTime(2019, 01, 01, 00, 00, 00);
            while (time.ToString("dd") != "02")
            {
                cmbbxStartTime.Items.Add(time.ToString("hh:mm tt"));
                cmbbxEndTime.Items.Add(time.ToString("hh:mm tt"));
                time = time.AddMinutes(15);
            }
            IsNewAppointment = false;
            //Populate the text boxes since we're dealing with an existing appointment
            txtbxDescription.Text = appoint.Description;
            txtbxLocation.Text = appoint.Location;
            txtbxContact.Text = appoint.Contact;
            txtbxTitle.Text = appoint.Title;
            txtbxURL.Text = appoint.Url;
            cmbbxStartTime.Text = appoint.Start.ToString("hh:mm tt");
            cmbbxEndTime.Text = appoint.End.ToString("hh:mm tt");
            dpStart.SelectedDate = appoint.Start;
            dpEnd.SelectedDate = appoint.End;
            appointment = appoint;
        }

        //Method for when the cancel button is clicked.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //Just close the form without modifying any data.
        }

        //Method for when the Ok button is clicked.  Save the modified data to the database.
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Appointment newAppointment = new Appointment();
            try
            {
                if (dpStart.SelectedDate.Value == null || cmbbxStartTime.SelectedValue == null)
                {
                    throw new Exception("Please enter an start date.");//Throw an exception if the user doesn't enter an start date
                }
                else
                {
                    newAppointment.Start = dpStart.SelectedDate.Value;
                    DateTime dateTime = DateTime.Parse(cmbbxStartTime.Text);
                    newAppointment.Start = newAppointment.Start.AddHours(dateTime.Hour);
                    newAppointment.Start = newAppointment.Start.AddMinutes(dateTime.Minute);
                    TimeZoneInfo tz = cmbbxTimeZone.SelectedValue == null ? TimeZoneInfo.Local : (TimeZoneInfo)cmbbxTimeZone.SelectedValue; //safety check
                    newAppointment.Start = TimeZoneInfo.ConvertTime(newAppointment.Start, tz, TimeZoneInfo.Utc); //Convert the time entered into UTC
                }
                if (dpEnd.SelectedDate.Value == null || cmbbxEndTime.Text == "")
                {
                    throw new Exception("Please enter an end date."); //Throw an exception if the user doesn't enter an end date
                }
                else
                {
                    newAppointment.End = dpEnd.SelectedDate.Value;
                    DateTime dateTime = DateTime.Parse(cmbbxEndTime.Text);
                    newAppointment.End = newAppointment.End.AddHours(dateTime.Hour);
                    newAppointment.End = newAppointment.End.AddMinutes(dateTime.Minute);
                    TimeZoneInfo tz = cmbbxTimeZone.SelectedValue == null ? TimeZoneInfo.Local : (TimeZoneInfo)cmbbxTimeZone.SelectedValue; //safety check
                    newAppointment.End = TimeZoneInfo.ConvertTime(newAppointment.End, tz, TimeZoneInfo.Utc); //Convert the time entered into UTC
                }
                if (dgrdCustomerList.SelectedItems.Count == 0)
                {
                    throw new Exception("Please select a customer from the list"); //Throw exception if the user doesn't select a customer
                }
                if(cmbxAppointmentType.SelectedItem == null)
                {
                    throw new Exception("Please enter an appointment type"); //Throw an exception if the user doesn't select an appointment type
                }
                if(newAppointment.Start > newAppointment.End)
                {
                    throw new Exception("The end time of the appointment is before the start time."); //Throw an exception if the appointment ends before it begins
                }
                Customer cust = (Customer)dgrdCustomerList.SelectedItem;
                newAppointment.CustomerId = cust.CustomerId;
                newAppointment.Contact = txtbxContact.Text;
                newAppointment.Description = txtbxDescription.Text;
                newAppointment.Title = txtbxTitle.Text;
                newAppointment.Location = txtbxLocation.Text;
                newAppointment.Url = txtbxURL.Text;
                AppointmentType appointmentType = (AppointmentType)cmbxAppointmentType.SelectedValue;
                newAppointment.AppointmentTypeId = appointmentType.TypeId;
                
                //Update Database and global structure with the new appointment or update the existing
                if(IsNewAppointment)
                {
                    newAppointment.AssociatedCustomer = MySQLDB.GetCustomerByID(newAppointment.CustomerId);
                    Globals.Appointments.Add(newAppointment);
                    newAppointment.UpdateDatabase(Properties.Settings.Default.CurrentUser);
                    
                }
                else
                {
                    newAppointment.AppointmentId = appointment.AppointmentId;
                    Globals.Appointments.Remove(appointment);
                    newAppointment.AssociatedCustomer = MySQLDB.GetCustomerByID(newAppointment.CustomerId);
                    newAppointment.UpdateDatabase(Properties.Settings.Default.CurrentUser);
                    Globals.Appointments.Add(newAppointment);
                }
                this.Close();
            }
            catch(Exception excep)
            {
                MessageBox.Show(excep.Message, "Insufficient Information", MessageBoxButton.OK, MessageBoxImage.Exclamation); //Show a dialog pointing out what the user didn't enter
            }
            
        }

        //Preselect the appropriate customer item if we're working with an existing appointment
        private void DgrdCustomerList_Loaded(object sender, RoutedEventArgs e)
        {
            if(!IsNewAppointment)
            {
                for (int i = 0; i < dgrdCustomerList.Items.Count; i++)
                {
                    Customer c = (Customer)dgrdCustomerList.Items.GetItemAt(i);
                    if (c.CustomerId == appointment.CustomerId)
                        dgrdCustomerList.SelectedIndex = i;
                }
            }
        }

        //Preselect the appropriate combobox item if we're working with an existing appointment
        private void CmbxAppointmentType_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsNewAppointment)
            {
                for (int i = 0; i < cmbxAppointmentType.Items.Count; i++)
                {
                    AppointmentType c = (AppointmentType)cmbxAppointmentType.Items.GetItemAt(i);
                    if (c.TypeId == appointment.AppointmentTypeId)
                        cmbxAppointmentType.SelectedIndex = i;
                }
            }
        }
    }
}
