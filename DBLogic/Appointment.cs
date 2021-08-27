using System;
using System.ComponentModel;

namespace DBLogic
{
    //Appointment class to closely match what is in the database.  INotifyPropertyChanged is used so that we can dynamically update databound controls.
    public class Appointment : INotifyPropertyChanged
    {
        //Private members of the class
        private int appointmentId;
        private int customerId;
        private string title;
        private string description;
        private string location;
        private string contact;
        private string url;
        private DateTime start;
        private DateTime end;
        private DateTime createDate;
        private string createdBy;
        private DateTime lastUpdate;
        private string lastUpdateBy;
        private Customer customer;
        private int appointmentTypeId;

        //Default Constructor
        public Appointment()
        {

        }

        //Constructor that allows to prepopulate the fields.
        public Appointment(int appointmentId, int customerId, int appointmentType, string title, string description, string location, string contact, string url, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            this.appointmentId = appointmentId;
            this.customerId = customerId;
            this.appointmentTypeId = appointmentType;
            customer = MySQLDB.GetCustomerByID(this.customerId);
            this.title = title;
            this.description = description;
            this.location = location;
            this.contact = contact;
            this.url = url;
            this.start = start;
            this.end = end;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }

        //Copy constructor
        public Appointment(Appointment appointment)
        {
            this.appointmentId = appointment.appointmentId;
            this.customerId = appointment.customerId;
            this.customer = MySQLDB.GetCustomerByID(this.customerId);
            this.title = appointment.title;
            this.description = appointment.description;
            this.appointmentTypeId = appointment.appointmentTypeId;
            this.location = appointment.location;
            this.contact = appointment.contact;
            this.url = appointment.url;
            this.start = appointment.start;
            this.end = appointment.end;
            this.createDate = appointment.createDate;
            this.createdBy = appointment.createdBy;
            this.lastUpdate = appointment.lastUpdate;
            this.lastUpdateBy = appointment.lastUpdateBy;
        }

        //For the INotifyPropertyChanged interface.  To update databindings when the information changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //Used to invoke the PropertyChangedEventHandler for INotifyPropertyChanged whenever something changes.
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Getter and Setter for AppointmentId with the proper notifications going out
        public int AppointmentId
        {
            get { return this.appointmentId; }
            set
            {
                if (this.appointmentId != value)
                {
                    this.appointmentId = value;
                    this.NotifyPropertyChanged("AppointmentId");
                }
            }
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
                    customer = MySQLDB.GetCustomerByID(value);
                }
            }
        }

        //Getter and Setter for CustomerId with the proper notifications going out
        public int AppointmentTypeId
        {
            get { return this.appointmentTypeId; }
            set
            {
                if (this.appointmentTypeId != value)
                {
                    this.appointmentTypeId = value;
                    this.NotifyPropertyChanged("AppointmentTypeId");
                    customer = MySQLDB.GetCustomerByID(value);
                }
            }
        }

        //Read-Only property that only gets updated when CustomerId is modified.
        public Customer AssociatedCustomer
        {
            get { return customer; }
            set
            {
                customer = value;
                this.customerId = customer.CustomerId;
                this.NotifyPropertyChanged("CustomerId");
            }
        }

        //Getter and Setter for Title with the proper notifications going out
        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.NotifyPropertyChanged("Title");
                }
            }
        }

        //Getter and Setter for Description with the proper notifications going out
        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        //Getter and Setter for Location with the proper notifications going out
        public string Location
        {
            get { return this.location; }
            set
            {
                if (this.location != value)
                {
                    this.location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        //Getter and Setter for Contact with the proper notifications going out
        public string Contact
        {
            get { return this.contact; }
            set
            {
                if (this.contact != value)
                {
                    this.contact = value;
                    this.NotifyPropertyChanged("Contact");
                }
            }
        }

        //Getter and Setter for URL with the proper notifications going out
        public string Url
        {
            get { return this.url; }
            set
            {
                if (this.url != value)
                {
                    this.url = value;
                    this.NotifyPropertyChanged("Url");
                }
            }
        }

        //Getter and Setter for Contact with the proper notifications going out
        public DateTime Start
        {
            get { return this.start; }
            set
            {
                if (this.start != value)
                {
                    this.start = value;
                    this.NotifyPropertyChanged("Start");
                }
            }
        }

        //Getter and Setter for End with the proper notifications going out
        public DateTime End
        {
            get { return this.end; }
            set
            {
                if (this.end != value)
                {
                    this.end = value;
                    this.NotifyPropertyChanged("End");
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
            if (this.appointmentId == 0)
            {
                MySQLDB.AddAppointment(this, currentUser);
            }
            else
            {
                MySQLDB.UpdateAppointment(this, currentUser);
            }
        }

        public void DeleteFromDatabase()
        {
            MySQLDB.DeleteAppointment(this);
        }
    }
}
