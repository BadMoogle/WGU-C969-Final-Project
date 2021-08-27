using System;
using System.ComponentModel;

namespace DBLogic
{
    //Customer class to closely match what is in the database.  INotifyPropertyChanged is used so that we can dynamically update databound controls.
    public class Customer : INotifyPropertyChanged
    {
        //Private members
        private int customerId;
        private string customerName;
        private int addressID;
        private int active;
        private DateTime createDate;
        private String createdBy;
        private DateTime lastUpdate;
        private String lastUpdatedBy;

        private Address address;

        //If the customer is active
        public bool IsActive
        {
            get
            {
                if (active != 0) //Since the activity state can technically be a non-binary value in the database, but we'll treat any non-zero as an active user
                    return true;
                else
                    return false;
            }
        }

        //Default Constructor
        public Customer()
        {
            customerId = 0;
            this.address = new Address();
        }

        //Fully loaded Constructor
        public Customer(int customerId, string customerName, int addressID, int active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.addressID = addressID;
            this.address = MySQLDB.GetAddressByID(addressID);
            this.active = active;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdatedBy = lastUpdatedBy;
        }

        //Copy Constructor
        public Customer(Customer cust)
        {
            this.customerId = cust.customerId;
            this.customerName = cust.customerName;
            this.addressID = cust.addressID;
            this.address = MySQLDB.GetAddressByID(cust.addressID);
            this.active = cust.active;
            this.createDate = cust.createDate;
            this.createdBy = cust.createdBy;
            this.lastUpdate = cust.lastUpdate;
            this.lastUpdatedBy = cust.lastUpdatedBy;
        }


        //For the INotifyPropertyChanged interface.  To update databindings when the information changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //Used to invoke the PropertyChangedEventHandler for INotifyPropertyChanged whenever something changes.
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Getter and Setter for CustomerId with the proper notifications going out
        public int CustomerId
        {
            get { return this.customerId; }
            set
            {
                if (this.customerId != value)
                {
                    this.customerId = value;
                    this.NotifyPropertyChanged("CustomerId");
                }
            }
        }

        //Getter and Setter for CustomerName with the proper notifications going out
        public string CustomerName
        {
            get { return this.customerName; }
            set
            {
                if (this.customerName != value)
                {
                    this.customerName = value;
                    this.NotifyPropertyChanged("CustomerName");
                }
            }
        }

        //Getter and Setter for AddressId with the proper notifications going out
        public int AddressId
        {
            get { return this.addressID; }
            set
            {
                if (this.addressID != value)
                {
                    this.addressID = value;
                    this.NotifyPropertyChanged("AddressID");
                    this.address = MySQLDB.GetAddressByID(value);
                }
            }
        }

        //Read-Only information on Address (to prevent unnecessary DB queries)
        public Address Address
        {
            get { return address; }
        }

        //Getter and Setter for Active with the proper notifications going out
        public int Active
        {
            get { return this.active; }
            set
            {
                if (this.active != value)
                {
                    this.active = value;
                    this.NotifyPropertyChanged("Active");
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

        //Getter and Setter for LasteUpdate with the proper notifications going out
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

        //Getter and Setter for LateUpdateBy with the proper notifications going out
        public string LastUpdatedBy
        {
            get { return this.lastUpdatedBy; }
            set
            {
                if (this.lastUpdatedBy != value)
                {
                    this.lastUpdatedBy = value;
                    this.NotifyPropertyChanged("LastUpdatedBy");
                }
            }
        }

        //Update the database
        public void UpdateDatabase(string currentUser)
        {
            if (this.CustomerId == 0)
            {
                MySQLDB.AddCustomer(this, currentUser);
            }
            else
            {
                MySQLDB.UpdateCustomer(this, currentUser);
            }
        }

        //Delete the customer from the database
        public void DeleteCustomerFromDatabase()
        {
            MySQLDB.DeleteCustomer(this);
        }

    }
}
