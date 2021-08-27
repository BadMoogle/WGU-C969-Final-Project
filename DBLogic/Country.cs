using System;
using System.ComponentModel;

namespace DBLogic
{
    //Country class to closely match what is in the database.  INotifyPropertyChanged is used so that we can dynamically update databound controls.
    public class Country : INotifyPropertyChanged
    {
        //Private members of the class
        private int countryId;
        private string country;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdateBy;

        //Default constructor
        public Country()
        {

        }

        //Fully loaded constructor
        public Country(int countryId, string country, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            this.countryId = countryId;
            this.country = country;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }

        //Copy Constructor
        public Country(Country country)
        {
            this.countryId = country.countryId;
            this.country = country.country;
            this.createDate = country.createDate;
            this.createdBy = country.createdBy;
            this.lastUpdate = country.lastUpdate;
            this.lastUpdateBy = country.lastUpdateBy;
        }

        //For the INotifyPropertyChanged interface.  To update databindings when the information changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //Used to invoke the PropertyChangedEventHandler for INotifyPropertyChanged whenever something changes.
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                }
            }
        }

        //Getter and Setter for CountryName with the proper notifications going out
        public string CountryName
        {
            get { return this.country; }
            set
            {
                if (this.country != value)
                {
                    this.country = value;
                    this.NotifyPropertyChanged("CountryName");
                }
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

        //Update the database
        public void UpdateDatabase(string currentUser)
        {

            if(this.CountryId == 0)
            {
                MySQLDB.AddCountry(this, currentUser);
            }
            else
            {
                //If the country name changed compared to what is in the database, probably best to just add a new record with it.
                if (MySQLDB.GetCountryByID(this.CountryId).CountryName != this.CountryName)
                {
                    MySQLDB.AddCountry(this, currentUser);
                }
                else
                {
                    MySQLDB.UpdateCountry(this, currentUser);
                }
            }
        }
    }
}
