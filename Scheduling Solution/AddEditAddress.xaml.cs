using System.Windows;
using DBLogic;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for AddEditAddress.xaml
    /// </summary>
    /// 
    public partial class AddEditAddress : Window
    {
        private Address currentAddress;
        private Address oldAddress;
        private bool IsNewAddress { get; set; }

        //Default constructor.  Implies entering a new address.
        public AddEditAddress()
        {
            InitializeComponent();
            cmbbxCity.ItemsSource = Globals.Cities;
            cmbbxCountry.ItemsSource = Globals.Countries;
            currentAddress = new Address();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            IsNewAddress = true;
        }

        //Contructor with an address object passed to it.  Means we're editing it.
        public AddEditAddress(Address addr)
        {
            InitializeComponent();
            this.Title = "Edit Address";
            txtbxAddress1.Text = addr.Address1;
            txtbxAddress2.Text = addr.Address2;
            txtbxPhone.Text = addr.Phone;
            txtbxPostalCode.Text = addr.PostalCode;
            cmbbxCity.ItemsSource = Globals.Cities;
            cmbbxCountry.ItemsSource = Globals.Countries;
            cmbbxCity.SelectedItem = addr.City;
            cmbbxCity.Text = addr.City.CityName;
            cmbbxCountry.SelectedItem = addr.City.Country;
            cmbbxCountry.Text = addr.City.Country.CountryName;
            currentAddress = addr;
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
            oldAddress = addr;
            IsNewAddress = false;
        }

        //Cancel button method.  Just closes the form without doing anything with the data.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Ok button click method.  Gathers the information entered and updates the database.
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            bool updatedCity = false;  //Used if we need to force a DB update on a city.  This is required due to the order we create the country/city objects.
            //If the user entered text into the combobox instead of selecting an item, its index will be -1
            //Create a new city object if the current doesn't exist
            if (cmbbxCity.SelectedIndex == -1)
            {
                currentAddress.City = new City
                {
                    CityName = cmbbxCity.Text
                };
                currentAddress.City.CityId = 0; //New country, different city
                updatedCity = true; //We force a db update on the city in the event we have a city name that exists in two different countries 
            }
            else
            {
                currentAddress.City = (City)cmbbxCity.SelectedItem;
            }

            //If the user entered text into the combobox instead of selecting an item, its index will be -1
            //Create a new country object if the current doesn't exist.  This has to come after the city combobox check otherwise the object gets remade.
            if (cmbbxCountry.SelectedIndex == -1)
            {
                currentAddress.City.Country = new Country
                {
                    CountryName = cmbbxCountry.Text,
                    CreatedBy = Properties.Settings.Default.CurrentUser,
                    LastUpdateBy = Properties.Settings.Default.CurrentUser
                };
                currentAddress.City.Country.UpdateDatabase(Properties.Settings.Default.CurrentUser);
                Globals.Countries.Add(currentAddress.City.Country);
            }
            else
            {
                currentAddress.City.Country = (Country)cmbbxCountry.SelectedItem;
            }
            //Force a DB update now that we have a new city and a proper country object.
            if(updatedCity)
            {
                currentAddress.City.UpdateDatabase(Properties.Settings.Default.CurrentUser); //In case a new city with a new country was entered.
                Globals.Cities.Add(currentAddress.City);
            }
            currentAddress.Address1 = txtbxAddress1.Text;
            currentAddress.Address2 = txtbxAddress2.Text;
            currentAddress.Phone = txtbxPhone.Text;
            currentAddress.PostalCode = txtbxPostalCode.Text;
            currentAddress.UpdateDatabase(Properties.Settings.Default.CurrentUser);
            //If editing an existing address, remove the existing in the collection and add the new one in in its place
            if(IsNewAddress)
            {
                Globals.Addresses.Add(currentAddress);
            }
            else
            {
                Globals.Addresses.Remove(oldAddress);
                Globals.Addresses.Add(currentAddress);
            }

            this.Close();
        }
    }
}
