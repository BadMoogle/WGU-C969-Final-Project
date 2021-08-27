using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DBLogic;

namespace Scheduling_Solution
{
    //Contains a copy of the information in the database.
    //I initiallly tried to avoid doing such a workaround, but the UI became unacceptably unresponsive querying the database at times.
    //Would be best to implement a local SQLite or some other type database for a cache, but that is beyond the scope of this project (as it would introduce additional libraries
    //prohibited by the scope of the project)
    public static class Globals
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public static ObservableCollection<Customer> Customers { get; set; }
        public static ObservableCollection<Address> Addresses { get; set; }
        public static ObservableCollection<City> Cities { get; set; }
        public static ObservableCollection<Country> Countries { get; set; }
        public static ObservableCollection<AppointmentType> AppointmentTypes { get; set; }

        //Task to update the database.  Will run every three minutes.
        public static async Task UpdateCollections()
        {
            Appointments = await Task.Run(() => MySQLDB.GetAllAppointments());
            Addresses = await Task.Run(() => MySQLDB.GetAllAddresses());
            Customers = await Task.Run(() => MySQLDB.GetCustomerList());
            Cities = await Task.Run(() => MySQLDB.GetCityList());
            Countries = await Task.Run(() => MySQLDB.GetCountryList());
            AppointmentTypes = await Task.Run(() => MySQLDB.GetAppointmentTypeList());
        }
    }
}
