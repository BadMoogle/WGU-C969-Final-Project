using System;
using System.ComponentModel;


namespace DBLogic
{
    //AppointmentType class to closely match what is in the database.  INotifyPropertyChanged is used so that we can dynamically update databound controls.
    public class AppointmentType : INotifyPropertyChanged
    {

        //Private members.  Not directly exposed publically.
        private int typeId;
        private string description;

        //Default Constructor
        public AppointmentType()
        {
            typeId = 0;
        }

        //Fully populated constructor
        public AppointmentType(int typeId, string description)
        {
            this.typeId = typeId;
            this.description = description;
        }

        //For the INotifyPropertyChanged interface.  To update databindings when the information changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //Used to invoke the PropertyChangedEventHandler for INotifyPropertyChanged whenever something changes.
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Getter and Setter for TypeId with the proper notifications going out
        public int TypeId
        {
            get { return this.typeId; }
            set
            {
                if (this.typeId != value)
                {
                    this.typeId = value;
                    this.NotifyPropertyChanged("TypeId");
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

        public static void AddAppointmentType(string type)
        {
            MySQLDB.AddAppointmentType(type);
        }
    }
}
