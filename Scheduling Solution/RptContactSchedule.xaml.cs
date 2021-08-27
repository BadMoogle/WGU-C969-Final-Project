using System.Linq;
using System.Windows;


namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for RptContactSchedule.xaml
    /// </summary>
    public partial class RptContactSchedule : Window
    {
        public RptContactSchedule()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            cmbxContact.ItemsSource = Globals.Appointments.Select(b => b.Contact).Distinct(); //Show unique contact entries
            cmbxContact.SelectedIndex = 0;
        }

        //Close the form
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Update the datagrid when the user changes the selection in the Contact box
        private void CmbxContact_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbxContact.SelectedValue != null) //safety
            {
                dgrdAppointmentList.ItemsSource = Globals.Appointments.Where(b => b.Contact == cmbxContact.SelectedValue.ToString());//get a list of the appointments that match the typeId
            }
        }
    }
}
