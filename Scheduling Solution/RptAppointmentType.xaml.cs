using System.Linq;
using System.Windows;
using DBLogic;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for RptAppointmentType.xaml
    /// </summary>
    public partial class RptAppointmentType : Window
    {
        public RptAppointmentType()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);

            cmbbxAppointmentType.ItemsSource = Globals.AppointmentTypes;
            cmbbxAppointmentType.SelectedIndex = 0;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmbbxAppointmentType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbbxAppointmentType.SelectedValue != null) //safety
            {
                AppointmentType type = (AppointmentType)cmbbxAppointmentType.SelectedValue;
                dgrdAppointmentList.ItemsSource = Globals.Appointments.Where(b => b.AppointmentTypeId == type.TypeId);//get a list of the appointments that match the typeId
            }
        }
    }
}
