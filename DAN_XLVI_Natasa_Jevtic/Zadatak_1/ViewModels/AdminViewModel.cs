﻿using System;
using System.Collections.Generic;
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
    class AdminViewModel : BaseViewModel
    {
        AdminView adminView;
        Calculation calculator = new Calculation();
        Validation validation = new Validation();
        Managers managers = new Managers();

        private vwManager manager;

        public vwManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<string> sectorList;

        public List<string> SectorList
        {
            get
            {
                return sectorList;
            }
            set
            {
                sectorList = value;
                OnPropertyChanged("SectorList");
            }
        }

        private List<string> accessLevelList;

        public List<string> AccessLevelList
        {
            get
            {
                return accessLevelList;
            }
            set
            {
                accessLevelList = value;
                OnPropertyChanged("AccessLevelList");
            }
        }


        private Visibility isVisibleAdding = Visibility.Hidden;
        public Visibility IsVisibleAdding
        {
            get
            {
                return isVisibleAdding;
            }
            set
            {
                isVisibleAdding = value;
                OnPropertyChanged("IsVisibleAdding");
            }
        }

        private ICommand addManager;
        public ICommand AddManager
        {
            get
            {
                if (addManager == null)
                {
                    addManager = new RelayCommand(param => AddManagerExecute(), param => CanAddManagerExecute());
                }
                return addManager;
            }
        }

        private ICommand logOut;
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
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

        public AdminViewModel(AdminView adminView)
        {
            this.adminView = adminView;
            Manager = new vwManager();
            SectorList = managers.GetSectors();
            AccessLevelList = managers.GetAccessLevel();
        }

        public void SaveExecute()
        {
            try
            {
                managers.AddManager(manager);
                IsVisibleAdding = Visibility.Hidden;
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
            if (!String.IsNullOrEmpty(Manager.Name) && !String.IsNullOrEmpty(Manager.Surname) && !String.IsNullOrEmpty(Manager.JMBG) && !String.IsNullOrEmpty(Manager.BankAccountNumber)
                && !String.IsNullOrEmpty(Manager.Email) && !String.IsNullOrEmpty(Manager.Position) && !String.IsNullOrEmpty(Manager.Username) && !String.IsNullOrEmpty(manager.Password) 
                && !String.IsNullOrEmpty(Manager.Sector) && !String.IsNullOrEmpty(manager.AccessLevel) && Int32.TryParse(manager.Salary.ToString(), out int number) && number > 0)
            {
                if (Manager.JMBG.Length == 13 && Manager.JMBG.All(Char.IsDigit) && Manager.BankAccountNumber.Length == 18 && Manager.BankAccountNumber.All(Char.IsDigit)
                    && calculator.CalculateDateOfBirth(manager.JMBG, out date) == true && validation.UniqueJMBG(Manager.JMBG) == true && validation.UniqueBankAccount(Manager.BankAccountNumber)==true 
                    && validation.UniqueEmail(Manager.Email)==true && validation.UniqueUsername(Manager.Username)== true)
                {

                    Manager.DateOfBirth = date;
                    if (validation.ValidationForEmail(manager.Email) == true)
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
                IsVisibleAdding = Visibility.Hidden;                
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

        public void LogOutExecute()
        {
            try
            {
                adminView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanLogOutExecute()
        {
            return true;
        }

        public void AddManagerExecute()
        {
            try
            {
                IsVisibleAdding = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanAddManagerExecute()
        {
            return true;
        }
    }
}