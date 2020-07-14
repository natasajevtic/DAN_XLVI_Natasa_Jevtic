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
    class AddEmployeeViewModel : BaseViewModel
    {
        AddEmployeeView addEmployeeView;
        Calculation calculator = new Calculation();
        Validation validation = new Validation();
        Employees employees = new Employees();

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

        public AddEmployeeViewModel(AddEmployeeView addEmployeeView)
        {
            this.addEmployeeView = addEmployeeView;
            employee = new vwEmployee();
        }

        public void SaveExecute()
        {
            try
            {
                employees.AddEmployee(employee);
                addEmployeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool CanSaveExecute()
        {
            DateTime date = DateTime.Now;
            //checks if user input data valid  
            if (!String.IsNullOrEmpty(Employee.Name) && !String.IsNullOrEmpty(Employee.Surname) && !String.IsNullOrEmpty(Employee.JMBG) && !String.IsNullOrEmpty(Employee.BankAccountNumber)
                && !String.IsNullOrEmpty(Employee.Email) && !String.IsNullOrEmpty(Employee.Position) && !String.IsNullOrEmpty(Employee.Username) && !String.IsNullOrEmpty(Employee.Password)
                && Int32.TryParse(Employee.Salary.ToString(), out int number) && number > 0)
            {
                if (Employee.JMBG.Length == 13 && Employee.JMBG.All(Char.IsDigit) && Employee.BankAccountNumber.Length == 18 && Employee.BankAccountNumber.All(Char.IsDigit)
                    && calculator.CalculateDateOfBirth(Employee.JMBG, out date) == true && validation.UniqueJMBG(Employee.JMBG) == true && validation.UniqueBankAccount(Employee.BankAccountNumber) == true
                    && validation.UniqueEmail(Employee.Email) == true && validation.UniqueUsername(Employee.Username) == true)
                {

                    Employee.DateOfBirth = date;
                    if (validation.ValidationForEmail(Employee.Email) == true)
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

        public void CancelExecute()
        {
            try
            {
                addEmployeeView.Close();
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