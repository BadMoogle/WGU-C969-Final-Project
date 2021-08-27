using System;
using System.ComponentModel;

namespace DBLogic
{
    //Address class to closely match what is in the database.  INotifyPropertyChanged is used so that we can dynamically update databound controls.
    public class Address : INotifyPropertyChanged
    {
        //Private fields
        private int addressId;
        private string address;
        private string address2;
        private int cityId;
        private string postalCode;
        private string phone;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdateBy;

        private City city; //Should only be modified when cityId is modified

        //For the INotifyPropertyChanged interface.  To update databindings when the information changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //Used to invoke the PropertyChangedEventHandler for INotifyPropertyChanged whenever something changes.
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Default constructor
        public Address()
        {
            AddressId = 0;
            this.city = new City();
        }

        //Constructor with all the fields
        public Address(int addressId, string address, string address2, int cityId, string postalCode, string phone, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            this.addressId = addressId;
            this.address = address;
            this.address2 = address2;
            this.cityId = cityId;
            this.city = MySQLDB.GetCityByID(cityId);
            this.postalCode = postalCode;
            this.phone = phone;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }

        //Copy constructor
        public Address(Address add)
        {
            this.addressId = add.addressId;
            this.address = add.address;
            this.address2 = add.address2;
            this.cityId = add.cityId;
            this.city = MySQLDB.GetCityByID(cityId);
            this.postalCode = add.postalCode;
            this.phone = add.phone;
            this.createDate = add.createDate;
            this.createdBy = add.createdBy;
            this.lastUpdate = add.lastUpdate;
            this.lastUpdateBy = add.lastUpdateBy;
        }

        public City City
        {
            get { return city; }
            set
            {
                city = value;
                this.CityId = city.CityId;
                this.NotifyPropertyChanged("CityId");
            }
        }


        //Getter and Setter for AddressId with the proper notifications going out
        public int AddressId
        {
            get { return this.addressId; }
            set
            {
                if (this.addressId != value)
                {
                    this.addressId = value;
                    this.NotifyPropertyChanged("AddressId");
                }
            }
        }

        //Getter and Setter for Address with the proper notifications going out
        public string Address1
        {
            get { return this.address; }
            set
            {
                if (this.address != value)
                {
                    this.address = value;
                    this.NotifyPropertyChanged("Address1");
                }
            }
        }

        //Getter and Setter for Address2 with the proper notifications going out
        public string Address2
        {
            get { return this.address2; }
            set
            {
                if (this.address2 != value)
                {
                    this.address2 = value;
                    this.NotifyPropertyChanged("Address2");
                }
            }
        }

        //Getter and Setter for CityId with the proper notifications going out
        public int CityId
        {
            get { return this.cityId; }
            set
            {
                if (this.cityId != value)
                {
                    this.cityId = value;
                    this.NotifyPropertyChanged("CityId");
                    city = MySQLDB.GetCityByID(value);
                }
            }
        }

        //Getter and Setter for PostalCode with the proper notifications going out
        public string PostalCode
        {
            get { return this.postalCode; }
            set
            {
                if (this.postalCode != value)
                {
                    this.postalCode = value;
                    this.NotifyPropertyChanged("PostalCode");
                }
            }
        }

        //Getter and Setter for Phone with the proper notifications going out
        public string Phone
        {
            get { return this.phone; }
            set
            {
                if (this.phone != value)
                {
                    this.phone = value;
                    this.NotifyPropertyChanged("Phone");
                }
            }
        }

        //Getter and Setter for CreateDate with the proper notifications going out
        public DateTime CreateDate
        {
            get { return this.CreateDate; }
            set
            {
                if (this.CreateDate != value)
                {
                    this.CreateDate = value;
                    this.NotifyPropertyChanged("CreateDate");
                }
            }
        }

        //Getter and Setter for CreatedBy with the proper notifications going out
        public string CreatedBy
        {
            get { return this.createdBy; }
            set
            {
                if (this.createdBy != value)
                {
                    this.createdBy = value;
                    this.NotifyPropertyChanged("CreatedBy");
                }
            }
        }

        //Getter and Setter for LastUpdate with the proper notifications going out
        public DateTime LastUpdate
        {
            get { return this.lastUpdate; }
            set
            {
                if (this.lastUpdate != value)
                { 
                    lastUpdate = value;
                    this.NotifyPropertyChanged("LastUpdate");
                }
            }
        }

        //Getter and Setter for CreatedBy with the proper notifications going out
        public string LastUpdateBy
        {
            get { return this.lastUpdateBy; }
            set
            {
                if (this.lastUpdateBy != value)
                {
                    this.lastUpdateBy = value;
                    this.NotifyPropertyChanged("LastUpdateBy");
                }
            }
        }

        //Updates the database.  Adds a new record if the ID is 0, otherwise updates the existing.
        public void UpdateDatabase(string currentUser)
        {
            if (this.AddressId == 0)
            {
                MySQLDB.AddAddress(this, currentUser);
            }
            else
            {
                MySQLDB.UpdateAddress(this, currentUser);
            }
        }
    }
}
