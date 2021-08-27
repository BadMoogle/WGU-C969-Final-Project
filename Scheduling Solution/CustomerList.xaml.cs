using System.Windows;
using System.Windows.Controls;
using DBLogic;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        //Default Constructor
        public CustomerList()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            dgrdCustomerList.ItemsSource = Globals.Customers;
        }

        //Close the form if the ok button is clicked.
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //If the user selects something from the customer list, activate the edit and delete buttons
        private void DgrdCustomerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrdCustomerList.SelectedItems.Count != 0)
            {
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
        }

        //Show the add customer box if the add button is clicked
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomer addEditCustomer = new AddEditCustomer();
            addEditCustomer.ShowDialog();
        }

        //Show the add customer box and feed it the customer object selected
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdCustomerList.SelectedItems.Count != 0)
            {
                Customer cust = (Customer)dgrdCustomerList.SelectedItem;
                AddEditCustomer addEditCustomer = new AddEditCustomer(cust);
                addEditCustomer.ShowDialog();
            }
        }

        //Delete the currently selected customer
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdCustomerList.SelectedItems.Count != 0)
            {
                Customer cust = (Customer)dgrdCustomerList.SelectedItem;
                Globals.Customers.Remove(cust);
                cust.DeleteCustomerFromDatabase();
            }
        }
    }
}
