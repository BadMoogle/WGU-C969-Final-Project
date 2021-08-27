using System.Linq;
using System.Windows;


namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for RptAppointmentsByCountry.xaml
    /// </summary>
    public partial class RptAppointmentsByCountry : Window
    {
        public RptAppointmentsByCountry()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            cmbxCountry.ItemsSource = Globals.Appointments.Select(b => b.AssociatedCustomer.Address.City.Country.CountryName).Distinct(); //Show unique country entries
            cmbxCountry.SelectedIndex = 0;
        }

        //Close the form
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Update the datagrid when the user changes the selection in the Country box
        private void CmbxCountry_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbxCountry.SelectedValue != null) //safety
            {
                dgrdAppointmentList.ItemsSource = Globals.Appointments.Where(b => b.AssociatedCustomer.Address.City.Country.CountryName == cmbxCountry.SelectedValue.ToString());//get a list of the appointments that match the country
            }
        }
    }
}
