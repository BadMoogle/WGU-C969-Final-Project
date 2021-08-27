using System;
using System.Linq;
using System.Windows;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for RptNumAppointTypeByMonth.xaml
    /// </summary>
    public partial class RptNumAppointTypeByMonth : Window
    {
        //Default constructor
        public RptNumAppointTypeByMonth()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            cldMonthToView.SelectedDate = DateTime.Now;
            UpdateDataGrid();
        }

        //Method for when the close button is clicked.
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        //For whenever the user selects a different day on the calendar
        private void CldMonthToView_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        //Update the datagrid with the information on what date was selected.  Broke this out into its own function because I was honestly thinking this would be more difficult.
        private void UpdateDataGrid()
        {
            dgrdAppointmentType.ItemsSource = from a in Globals.AppointmentTypes select new { AppointType = a.Description, Number = Globals.Appointments.Where((b) => 
                (a.TypeId == b.AppointmentTypeId) && (b.Start.ToString("yyyy-MM") == cldMonthToView.SelectedDate.GetValueOrDefault().ToString("yyyy-MM"))).Count() }; //Select those on the same month/year and the count of those occurring
        }
    }
}
