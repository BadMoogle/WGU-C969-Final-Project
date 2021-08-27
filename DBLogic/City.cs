using System;
using System.ComponentModel;

namespace DBLogic
{
    //City class to closely match what is in the database.  INotifyPropertyChanged is used so that we can dynamically update databound controls.
    public class City : INotifyPropertyChanged
    {
        //Private members.  Not directly exposed publically.
        private int cityId;
        private string city;
        private int countryId;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdateBy;
        private Country country;

        //Default Constructor
        public City()
        {
            CityId = 0;
            countryId = 0;
            this.Country = new Country();
        }

        //Fully populated constructor
        public City(int cityId, string city, int countryId, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            this.cityId = cityId;
            this.city = city;
            this.countryId = countryId;
            this.Country = MySQLDB.GetCountryByID(countryId);
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }

        //For the INotifyPropertyChanged interface.  To update databindings when the information changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //Used to invoke the PropertyChangedEventHandler for INotifyPropertyChanged whenever something changes.
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                }
            }
        }

        //Getter and Setter for CityName with the proper notifications going out
        public string CityName
        {
            get { return this.city; }
            set
            {
                if (this.city != value)
                {
                    this.city = value;
                    this.NotifyPropertyChanged("CityName");
                }
            }
        }

        //Getter and Setter for CountryId with the proper notifications going out
        public int CountryId
        {
            get { return this.countryId; }
            set
            {
                if (this.countryId != value)
                {
                    this.countryId = value;
                    this.NotifyPropertyChanged("CountryId");
                    this.Country = MySQLDB.GetCountryByID(value);
                }
            }
        }

        //Read-Only conuntry that is only changed via CountryId
        public Country Country
        {
            get { return country; }
            set
            {
                country = value;
                CountryId = value.CountryId;
                this.NotifyPropertyChanged("CountryId");
            }
        }

        //Getter and Setter for CreateDate with the proper notifications going out
        public DateTime CreateDate
        {
            get { return this.createDate; }
            set
            {
                if (this.createDate != value)
                {
                    this.createDate = value;
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
                    this.lastUpdate = value;
                    this.NotifyPropertyChanged("LastUpdate");
                }
            }
        }

        //Getter and Setter for LastUpdateBy with the proper notifications going out
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
            if(this.CityId == 0)
            {
                MySQLDB.AddCity(this, currentUser);
            }
            else
            {
                MySQLDB.UpdateCity(this, currentUser);
            }
        }

    }
}
