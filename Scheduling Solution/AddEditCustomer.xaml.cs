using System;
using System.Windows;
using System.Windows.Controls;
using DBLogic;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class AddEditCustomer : Window
    {
        private Customer customer;
        private bool IsNewCustomer { get; set; }

        //Default constructor.  Used for adding new customers to the database
        public AddEditCustomer()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            dgrdAddressList.ItemsSource = Globals.Addresses;
            customer = new Customer();
            IsNewCustomer = true;
        }

        //Constructor for existing customers.  Changes the window title and prepopulates the data.
        public AddEditCustomer(Customer cust)
        {
            InitializeComponent();
            this.Title = "Edit Customer";
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            //populate the text boxes if this is an existing customer
            dgrdAddressList.ItemsSource = Globals.Addresses;
            customer = cust;
            txtbxCustomerName.Text = customer.CustomerName;
            chkbxIsActive.IsChecked = customer.IsActive;
            IsNewCustomer = false;
        }

        //Method for handling the Ok button being clicked.
        //It will fill out the customer object and then either add or update the database with the various fields.
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Throw an exception if the user doesn't provide enough information
                if(txtbxCustomerName.Text == "")
                {
                    throw new Exception("Please enter a customer name");
                }
                if(dgrdAddressList.SelectedItem == null)
                {
                    throw new Exception("Please select an address from the list");
                }
                customer.CustomerName = txtbxCustomerName.Text;
                customer.CreateDate = DateTime.UtcNow;
                customer.Active = chkbxIsActive.IsEnabled ? 1 : 0;
                Address addr = (Address)dgrdAddressList.SelectedItem;
                customer.AddressId = addr.AddressId;
                customer.UpdateDatabase(Properties.Settings.Default.CurrentUser);
                Globals.Customers.Add(customer);
                this.Close();
            }
            catch(Exception excep)
            {
                MessageBox.Show(excep.Message, "Insufficient Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //Method for the cancel button being clicked.  Just closes the form.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //If the Add new address button is clicked
        private void BtnAddAddress_Click(object sender, RoutedEventArgs e)
        {
            AddEditAddress addressWindow = new AddEditAddress();
            addressWindow.ShowDialog();
        }

        //If the edit address button is clicked, edit the address
        private void BtnEditAddress_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdAddressList.SelectedItem != null)
            {
                Address addr = (Address)dgrdAddressList.SelectedItem;
                AddEditAddress addEditAddress = new AddEditAddress(addr);
                addEditAddress.ShowDialog();
            }
        }

        //Enable the edit box when the user selects an address in the list
        private void DgrdAddressList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditAddress.IsEnabled = (dgrdAddressList.SelectedItems.Count > 0) ? true : false; //Enable the edit button if the user selects something
        }

        //Set the selected item to what is in the customer object if this is an existing customer
        private void DgrdAddressList_Loaded(object sender, RoutedEventArgs e)
        {
            if(!IsNewCustomer)
            {
                for (int i = 0; i < dgrdAddressList.Items.Count; i++)
                {
                    Address c = (Address)dgrdAddressList.Items.GetItemAt(i);
                    if (c.AddressId == customer.AddressId)
                        dgrdAddressList.SelectedIndex = i;
                }
            }
        }
    }
}
