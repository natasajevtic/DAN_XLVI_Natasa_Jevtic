using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Calculations;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Validations;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EditEmployeeViewModel : BaseViewModel
    {
        EditEmployeeView editEmployeeView;
        Calculation calculator = new Calculation();
        Validation validation = new Validation();
        Employees employees = new Employees();
        public vwEmployee CheckIsEmployeeChanged { get; set; }

        private vwEmployee employee;

        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }
        public EditEmployeeViewModel(EditEmployeeView editEmployeeView, vwEmployee employeeToEdit)
        {
            this.editEmployeeView = editEmployeeView;
            this.employee = employeeToEdit;
            //gets employee initial values before editing
            CheckIsEmployeeChanged = new vwEmployee
            {
                Name = employeeToEdit.Name,
                Surname = employeeToEdit.Surname,
                JMBG = employeeToEdit.JMBG,
                BankAccountNumber = employeeToEdit.BankAccountNumber,
                Email = employeeToEdit.Email,
                Salary = employeeToEdit.Salary,
                Position = employeeToEdit.Position,
                Username = employeeToEdit.Username,
                Password = employeeToEdit.Password
            };
        }
        /// <summary>
        /// This method invokes a methods for editing employee achecks if sector of employee exists. If not exist, invokes a method for adding sector.
        /// </summary>
        public void SaveExecute()
        {
            try
            {
                employees.EditEmployee(employee);
                editEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>       
        /// This method checks if employee data is changed. If changed, checks if user input data is valid.
        /// </summary>
        /// <returns>True if data is changed and valid, false if not.</returns>  
        public bool CanSaveExecute()
        {
            DateTime date = DateTime.Now;
            //checks if user input data changed and valid               
            if ((employee.Name != CheckIsEmployeeChanged.Name || employee.Surname != CheckIsEmployeeChanged.Surname || employee.BankAccountNumber != CheckIsEmployeeChanged.BankAccountNumber ||
                      employee.JMBG != CheckIsEmployeeChanged.JMBG || employee.Salary != CheckIsEmployeeChanged.Salary || employee.Position != CheckIsEmployeeChanged.Position
                      || employee.Email != CheckIsEmployeeChanged.Email || employee.Username != CheckIsEmployeeChanged.Username || employee.Password != CheckIsEmployeeChanged.Password)
                 &&
                 !String.IsNullOrEmpty(employee.Name) && !String.IsNullOrEmpty(employee.Surname) && !String.IsNullOrEmpty(Employee.JMBG) && !String.IsNullOrEmpty(Employee.BankAccountNumber) && !String.IsNullOrEmpty(employee.Email)
                 && Int32.TryParse(employee.Salary.ToString(), out int number) && number > 0 && !String.IsNullOrEmpty(employee.Position) && !String.IsNullOrEmpty(employee.Username) && !String.IsNullOrEmpty(employee.Password))

            {
                if (Employee.BankAccountNumber.Length == 18 && Employee.BankAccountNumber.All(Char.IsDigit) && Employee.JMBG.Length == 13 && Employee.JMBG.All(Char.IsDigit) &&
                     calculator.CalculateDateOfBirth(employee.JMBG, out date) == true && validation.UniqueUsername(Employee.Username, CheckIsEmployeeChanged.Username)==true &&
                     validation.UniqueJMBG(Employee.JMBG, CheckIsEmployeeChanged.JMBG)==true && validation.UniqueEmail(Employee.Email, CheckIsEmployeeChanged.Email)==true &&
                     validation.UniqueBankAccount(Employee.BankAccountNumber, CheckIsEmployeeChanged.BankAccountNumber)==true)
                {
                    if (validation.ValidationForEmail(employee.Email) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for closing window of editing employee.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                editEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }
    }
}