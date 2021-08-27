using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DBLogic
{
    public class MySQLDB
    {
        //Adds an address object to the MySQL Database
        public static void AddAddress(Address addr, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT MAX(addressId) from address";
                MySqlDataReader query = cmd.ExecuteReader();
                int nextID = 0;
                query.Read();
                //If MAX is ran on a database with no rows, then it returns null and the GetUnt16 will throw an exception.
                if (!query.IsDBNull(0))
                {
                    nextID = query.GetUInt16(0);
                    nextID++;
                }
                else
                {
                    nextID = 1;
                }
                addr.AddressId = nextID;
                dbConn.Close();
                //Close the connection and open a new one since it wasn't letting me execute multiple queries on the same connection.
                dbConn.Open();
                cmd = dbConn.CreateCommand();
                /*cmd.CommandText = "INSERT INTO address ('addressId', 'address', 'address2', 'cityId', 'postalCode', 'phone', 'createDate', 'createdBy', 'lastUpdateBy') VALUES ('"
                    + nextID + "', '" + addr.Address1 + "', '" + addr.Address2 + "', '" + addr.CityId + "', '" + addr.PostalCode + "', '" + addr.Phone + "', '"
                    + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + currentUser + "', '" + currentUser + "')"; */
                cmd.CommandText = "INSERT INTO address (`addressId`, `address`, `address2`, `cityId`, `postalCode`, `phone`, `createDate`, `createdBy`, `lastUpdate`, `lastUpdateBy`) VALUES('"
                    + nextID + "', '" + addr.Address1 + "', '" + addr.Address2 + "', '" + addr.CityId + "', '" + addr.PostalCode + "', '" + addr.Phone + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + 
                    "', '" + currentUser + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + currentUser + "');";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        //Adds an Appointment object to the MySQL Database
        public static void AddAppointment(Appointment appointment, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT MAX(appointmentId) from appointment";
                MySqlDataReader query = cmd.ExecuteReader();
                int nextID = 0;
                query.Read();
                //If MAX is ran on a database with no rows, then it returns null and the GetUnt16 will throw an exception.
                if (!query.IsDBNull(0))
                {
                    nextID = query.GetUInt16(0);
                    nextID++;
                }
                else
                {
                    nextID = 1;
                }
                appointment.AppointmentId = nextID;
                dbConn.Close();
                //Close the connection and open a new one since it wasn't letting me execute multiple queries on the same connection.
                dbConn.Open();
                
                cmd = dbConn.CreateCommand();
                cmd.CommandText = "INSERT INTO appointment (appointmentId, customerId, appointmentType, title, description, location, contact, url, start, end, createDate, createdBy, lastUpdateBy) VALUES ('"
                    + nextID + "', '" + appointment.CustomerId + "', '" + appointment.AppointmentTypeId + "', '"  + appointment.Title + "', '" + appointment.Description + "', '" + appointment.Location 
                    + "', '" + appointment.Contact + "', '" + appointment.Url + "', '" + DateTime.SpecifyKind(appointment.Start, DateTimeKind.Utc).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.SpecifyKind(appointment.End, DateTimeKind.Utc).ToString("yyyy-MM-dd HH:mm:ss")
                    + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + currentUser + "', '" + currentUser + "')";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        //Adds an Appointment object to the MySQL Database
        public static void AddAppointmentType(string appointment)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Close the connection and open a new one since it wasn't letting me execute multiple queries on the same connection.
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd = dbConn.CreateCommand();
                cmd.CommandText = "INSERT INTO appointmenttype (TypeDescription) VALUES ('" + appointment + "')";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        //Adds a City object to the MySQL Database
        public static void AddCity(City city, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT MAX(cityId) from city";
                MySqlDataReader query = cmd.ExecuteReader();
                int nextID = 0;
                query.Read();
                //If MAX is ran on a database with no rows, then it returns null and the GetUnt16 will throw an exception.
                if (!query.IsDBNull(0))
                {
                    nextID = query.GetUInt16(0);
                    nextID++;
                }
                else
                {
                    nextID = 1;
                }
                city.CityId = nextID;
                dbConn.Close();
                //Close the connection and open a new one since it wasn't letting me execute multiple queries on the same connection.
                dbConn.Open();
                cmd = dbConn.CreateCommand();
                cmd.CommandText = "INSERT INTO city (`cityId`, `city`, `countryId`, `createDate`, `createdBy`, `lastUpdateBy`) VALUES ('"
                    + nextID + "', '" + city.CityName + "', '" + city.CountryId + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + currentUser + "', '" + currentUser + "')";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        //Adds an country object to the MySQL Database
        public static void AddCountry(Country country, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT MAX(countryId) from country";
                MySqlDataReader query = cmd.ExecuteReader();
                int nextID = 0;
                query.Read();
                //If MAX is ran on a database with no rows, then it returns null and the GetUnt16 will throw an exception.
                if (!query.IsDBNull(0))
                {
                    nextID = query.GetUInt16(0);
                    nextID++;
                }
                else
                {
                    nextID = 1;
                }
                country.CountryId = nextID;
                dbConn.Close();
                //Close the connection and open a new one since it wasn't letting me execute multiple queries on the same connection.
                dbConn.Open();
                cmd = dbConn.CreateCommand();
                cmd.CommandText = "INSERT INTO country (`countryId`, `country`, `createDate`, `createdBy`, `lastUpdateBy`) VALUES ('"
                    + nextID + "', '" + country.CountryName + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + currentUser + "', '" + currentUser + "')";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        //Adds an customer object to the MySQL Database
        public static void AddCustomer(Customer cust, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT MAX(customerId) from customer";
                MySqlDataReader query = cmd.ExecuteReader();
                int nextID = 0;
                query.Read();
                //If MAX is ran on a database with no rows, then it returns null and the GetUnt16 will throw an exception.
                if (!query.IsDBNull(0))
                {
                    nextID = query.GetUInt16(0);
                    nextID++;
                }
                else
                {
                    nextID = 1;
                }
                cust.CustomerId = nextID;
                dbConn.Close();
                //Close the connection and open a new one since it wasn't letting me execute multiple queries on the same connection.
                dbConn.Open();
                cmd = dbConn.CreateCommand();
                cmd.CommandText = "INSERT INTO customer (`customerId`, `customerName`, `addressId`, `active`, `createDate`, `createdBy`, `lastUpdateBy`) VALUES ('"
                    + nextID + "', '" + cust.CustomerName + "', '" + cust.AddressId + "', '" + cust.Active + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                    + "', '" + currentUser + "', '" + currentUser + "')";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void DeleteAddress(Address addr)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM address WHERE 'addressId'=" + addr.AddressId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void DeleteAppointment(Appointment appointment)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM appointment WHERE appointmentId='" + appointment.AppointmentId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void DeleteAppointment(int appointmentId)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM appointment WHERE appointmentId='" + appointmentId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void DeleteCity(City city, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM city WHERE cityId='" + city.CityId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void DeleteCountry(Country country)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM country WHERE countryId='" + country.CountryId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void DeleteCustomer(Customer cust)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM customer WHERE customerID='" + cust.CustomerId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static Address GetAddressByID(int addressId)
        {
            Address addr = new Address();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM address WHERE addressId='" + addressId + "'";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    addr = new Address(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetInt32(3), query.GetString(4), query.GetString(5), query.GetDateTime(6), 
                        query.GetString(7), query.GetDateTime(8), query.GetString(9));
                }
                dbConn.Close();
            }
            return addr;
        }

        //Returns a collection of the entire address list
        public async static Task<ObservableCollection<Address>> GetAllAddresses()
        {
            ObservableCollection<Address> addressList = new ObservableCollection<Address>();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM address";
                MySqlDataReader query = await Task.Run(() => cmd.ExecuteReader());
                while (query.Read())
                {
                    addressList.Add(new Address(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetInt32(3), query.GetString(4), query.GetString(5), query.GetDateTime(6),
                        query.GetString(7), query.GetDateTime(8), query.GetString(9)));
                }
                dbConn.Close();
            }
            return addressList;
        }

        public async static Task<ObservableCollection<Appointment>> GetAllAppointments()
        {
            ObservableCollection<Appointment> appointmnetList = new ObservableCollection<Appointment>();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT appointmentId, customerId, appointmentType, title, description, location, contact, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy FROM appointment";
                MySqlDataReader query = await Task.Run(() => cmd.ExecuteReader());
                while (query.Read())
                {
                    appointmnetList.Add(new Appointment(query.GetInt32(0), query.GetInt32(1), query.GetInt32(2), query.GetString(3), query.GetString(4), query.GetString(5), query.GetString(6),
                        query.GetString(7), query.GetDateTime(8), query.GetDateTime(9), query.GetDateTime(10), query.GetString(11), query.GetDateTime(12), query.GetString(13)));
                }
                dbConn.Close();
            }
            return appointmnetList;
        }

        public static ObservableCollection<AppointmentType> GetAppointmentTypeList()
        {
            ObservableCollection<AppointmentType> countryList = new ObservableCollection<AppointmentType>();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM appointmenttype";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    countryList.Add(new AppointmentType(query.GetInt32(0), query.GetString(1)));
                }
                dbConn.Close();
            }
            return countryList;
        }


        public static City GetCityByID(int cityId)
        {
            City city = new City();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM city WHERE cityId='" + cityId + "'";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    city = new City(query.GetInt32(0), query.GetString(1), query.GetInt32(2), query.GetDateTime(3), query.GetString(4), query.GetDateTime(5), query.GetString(6));
                }
                dbConn.Close();
            }
            return city;
        }

        public static ObservableCollection<City> GetCityList()
        {
            ObservableCollection<City> cityList = new ObservableCollection<City>();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM city";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    cityList.Add(new City(query.GetInt32(0), query.GetString(1), query.GetInt32(2), query.GetDateTime(3), query.GetString(4), query.GetDateTime(5), query.GetString(6)));
                }
                dbConn.Close();
            }
            return cityList;
        }

        public static Country GetCountryByID(int countryId)
        {
            Country country = new Country();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM country WHERE countryId='" + countryId + "'";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    country = new Country(query.GetInt32(0), query.GetString(1), query.GetDateTime(2), query.GetString(3), query.GetDateTime(4), query.GetString(5));
                }
                dbConn.Close();
            }
            return country;
        }

        public static ObservableCollection<Country> GetCountryList()
        {
            ObservableCollection<Country> countryList = new ObservableCollection<Country>();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM country";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    countryList.Add(new Country(query.GetInt32(0), query.GetString(1), query.GetDateTime(2), query.GetString(3), query.GetDateTime(4), query.GetString(5)));
                }
                dbConn.Close();
            }
            return countryList;
        }


        public static Customer GetCustomerByID(int id)
        {
            Customer cust = new Customer();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM customer WHERE customerId='" + id + "'";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    cust.CustomerId = query.GetInt32(0);
                    cust.CustomerName = query.GetString(1);
                    cust.AddressId = query.GetInt32(2);
                    cust.Active = query.GetInt32(3);
                    cust.CreateDate = query.GetDateTime(4);
                    cust.CreatedBy = query.GetString(5);
                    cust.LastUpdate = query.GetDateTime(6);
                    cust.LastUpdatedBy = query.GetString(7);
                }
                dbConn.Close();
            }
            return cust;
        }

        public static ObservableCollection<Customer> GetCustomerList()
        {
            ObservableCollection<Customer> cust = new ObservableCollection<Customer>();
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM customer";
                MySqlDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    cust.Add(new Customer(query.GetInt32(0), query.GetString(1), query.GetInt32(2), query.GetInt32(3), query.GetDateTime(4), query.GetString(5), query.GetDateTime(6), query.GetString(7)));
                }
                dbConn.Close();
            }
            return cust;
        }

        //Create a new directory in %appdata% called "Scheduling Solution\Log Files" and write the login attempts there.
        private static void LogLoginEvent(String userID, bool authResult)
        {
            String workingDirectory = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "Scheduling Solution");
            workingDirectory = System.IO.Path.Combine(workingDirectory, "Log Files");
            try
            {
                System.IO.Directory.CreateDirectory(workingDirectory);
                String logFile = System.IO.Path.Combine(workingDirectory, "Login.txt");
                if (authResult)
                {
                    System.IO.File.AppendAllText(logFile, "[" + DateTime.UtcNow + " UTC] " + userID + " successfully logged in.\n");
                }
                else
                {
                    System.IO.File.AppendAllText(logFile, "[" + DateTime.UtcNow + " UTC] " + userID + " failed a logged in attempt.\n");
                }

            }
            catch
            {
                throw new Exception("Unable to write to log files");
            }
        }

        public static void UpdateAddress(Address addr, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "UPDATE address SET address='" + addr.Address1 + "', address2='" + addr.Address2 + "', cityId='" + addr.CityId +
                    "', postalCode='" + addr.PostalCode + "', phone='" + addr.Phone + "', lastUpdateBy='" + currentUser + "' WHERE addressId='" + addr.AddressId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void UpdateAppointment(Appointment appointment, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "UPDATE appointment SET customerId='" + appointment.CustomerId + "', appointmentType='" + appointment.AppointmentTypeId + "', title='" + appointment.Title + "', description='" + appointment.Description +
                    "', location='" + appointment.Location + "', contact='" + appointment.Contact + "', url='" + appointment.Url + "', start='" + DateTime.SpecifyKind(appointment.Start, DateTimeKind.Utc).ToString("yyyy-MM-dd HH:mm:ss") +
                    "', end='" + DateTime.SpecifyKind(appointment.End, DateTimeKind.Utc).ToString("yyyy-MM-dd HH:mm:ss") + "', lastUpdateBy='" + currentUser + "' WHERE appointmentId='" + appointment.AppointmentId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void UpdateCity(City city, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "UPDATE city SET `city`='" + city.CityName + "', `countryId`='" + city.CountryId + "', `lastUpdateBy`='" + currentUser + "' WHERE 'cityId'='" + city.CityId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void UpdateCountry(Country country, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "UPDATE country SET `country`='" + country.CountryName + "', `lastUpdateBy`='" + currentUser + "' WHERE 'countryId'='" + country.CountryId + "'";
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void UpdateCustomer(Customer cust, string currentUser)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                cmd.CommandText = "UPDATE customer SET customerName='"+ cust.CustomerName + "', addressId='" + cust.AddressId + "', active='" + cust.Active + 
                    "', lastUpdateBy='" + currentUser + "' WHERE customerID='" + cust.CustomerId + "'" ;
                cmd.ExecuteNonQueryAsync();
                dbConn.Close();
            }
        }

        public static void VerifyLogin(String userID, String password)
        {
            using (MySqlConnection dbConn = new MySqlConnection(Properties.Settings.Default.MySQLConnectionInfo))
            {
                //Open the database connection
                dbConn.Open();
                MySqlCommand cmd = dbConn.CreateCommand();
                //Set up the query for the userName, password, and if the user is active in the database
                cmd.CommandText = "SELECT userName, password, active FROM user WHERE userName='" + userID + "'";
                MySqlDataReader query = cmd.ExecuteReader();
                bool passAuthentication = false;          
                //Set this as a reading loop since technically it's possible to have more than one username that's identical in the database.
                while (query.Read())
                {
                    // Check if the uppercase username matches what was passed, that the password matches, and that we have an active user
                    if (query.GetString(0).ToUpper() == userID.ToUpper() && query.GetString(1) == password && query.GetUInt16(2) != 0)
                    {
                        passAuthentication = true;
                    }
                }
                dbConn.Close();
                LogLoginEvent(userID, passAuthentication);
                if(passAuthentication == false)
                {
                    throw new Exception("Invalid Username or Password");
                }
            }
        }
    }
}
