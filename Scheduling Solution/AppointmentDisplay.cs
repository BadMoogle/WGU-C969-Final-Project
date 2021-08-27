using System.ComponentModel;


namespace Scheduling_Solution
{
    public class AppointmentDisplay : INotifyPropertyChanged
    {
        //Private members of the class
        private int appointmentId;
        private string customerName;
        private string start;
        private string end;
        private string contact;

        //Default Constructor
        public AppointmentDisplay()
        {

        }

        //Constructor that allows to prepopulate the fields.
        public AppointmentDisplay(int appointmentId, string customerName, string contact, string start, string end)
        {
            this.appointmentId = appointmentId;
            this.customerName = customerName;
            this.start = start;
            this.end = end;
            this.contact = contact;
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


        //Getter and Setter for Contact with the proper notifications going out
        public string Start
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
        public string End
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
    }
}
