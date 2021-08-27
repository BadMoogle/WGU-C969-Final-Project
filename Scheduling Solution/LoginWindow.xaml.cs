using System.Windows;

namespace Scheduling_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //Center the window on the screen
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
        }

        //Method for when the exit button is clicked
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Method for when the login button is clicked
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Check if the user has put in a valid username or password.  If not then display a message.
            try
            {
                DBLogic.MySQLDB.VerifyLogin(txtbxUserID.Text, txtbxPassword.Password); //Verifies if the username and password are correct.  Throws an exception if not
                Properties.Settings.Default.CurrentUser = txtbxUserID.Text.ToString(); //Stores the currently logged in user ID in a global variable for updating the database when they modify something
                LoadingDataWindow wndLoadingData = new LoadingDataWindow();
                wndLoadingData.Show();
                this.Close();
            }
            catch
            {
                //Display an error if the username/password is incorrect
                MessageBox.Show(Properties.Resources.InvalidUsernameOrPassword, Properties.Resources.InvalidUsernameOrPassword, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Press the login button if enter is pressed
        private void TxtbxPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                this.BtnLogin_Click(this, null);
            }
        }

        //Press the login button if enter is pressed
        private void TxtbxUserID_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.BtnLogin_Click(this, null);
            }
        }

        //Set the focus to the userID field when the window is rendered
        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            txtbxUserID.Focus();
        }
    }
}
