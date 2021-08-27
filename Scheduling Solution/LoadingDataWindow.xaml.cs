using System.Windows;
using System.Threading.Tasks;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for LoadingDataWindowxaml.xaml
    /// </summary>
    public partial class LoadingDataWindow : Window
    {
        public LoadingDataWindow()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
        }

        //Begins loading the data into the Globals data structures.  Front loading the data makes the UI considerably more responsive than querying the database with each new form or update.
        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            Task task = Task.Run(async () => { await Globals.UpdateCollections(); }); //Begin the task of populating the global collections
            //When the task completes, close out the loading form and load the main window.  TaskScheduler.FromCurrentSynchronizationContext forces the task to run on the UI thread.
            task.ContinueWith((t) => {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }, TaskScheduler.FromCurrentSynchronizationContext()); 
        }
    }
}
