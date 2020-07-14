using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow main;
        Employees employees = new Employees();
        Managers managers = new Managers();

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }

        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public vwEmployee Employee { get; set; }
        public vwManager Manager { get; set; }


        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => LogInExecute(), param => CanLogInExecute());
                }
                return logIn;
            }
        }
        public MainWindowViewModel(MainWindow main)
        {
            this.main = main;
        }
        /// <summary>
        /// This method invokes method for showing window based on user role.
        /// </summary>
        public void LogInExecute()
        {
            if (Username == "WPFadmin" && Password == "WPFadmin")
            {
                AdminView admin = new AdminView();
                admin.ShowDialog();
            }
            else if (employees.FindEmployee(Username, Password) != null)
            {
                Employee = employees.FindEmployee(Username, Password);
                EmployeeView employeeView = new EmployeeView(Employee);
                employeeView.ShowDialog();
            }
            else
            {
                Manager = managers.FindManager(Username, Password);
                ManagerView managerView = new ManagerView();
                managerView.ShowDialog();
            }
        }
        /// <summary>
        /// This method checks can login executes.
        /// </summary>
        /// <returns></returns>
        public bool CanLogInExecute()
        {
            if (Username == "WPFadmin" && Password == "WPFadmin")
            {
                return true;
            }
            else if (employees.AuthenticateEmployee(Username, Password) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}