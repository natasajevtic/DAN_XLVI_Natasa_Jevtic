using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Zadatak_1.Models;

namespace Zadatak_1.Validations
{
    class Validation
    {
        /// <summary>
        /// This method checks if email adress is valid.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if valid, false if not.</returns>
        public bool ValidationForEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }        
        /// <summary>
        /// This method checks if forwarded jmbg unique.
        /// </summary>
        /// <param name="jmbg">Employee jmbg.</param>
        /// <returns>True if unique, false if not.</returns>
        public bool UniqueJMBG(string jmbg)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            var list = employeeList.Where(x => x.JMBG == jmbg).ToList();
            //if exists employee with forwarded jmbg, return false
            if (list.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// This method checks if forwarded bank account unique.
        /// </summary>
        /// <param name="bankAccount">Employee bank account.</param>
        /// <returns>True if unique, false if not.</returns>
        public bool UniqueBankAccount(string bankAccount)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            var list = employeeList.Where(x => x.BankAccountNumber == bankAccount).ToList();
            //if exists employee with forwarded bank account, return false
            if (list.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// This method checks if forwarded username unique.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UniqueUsername(string username)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            var list = employeeList.Where(x => x.Username == username).ToList();
            //if exists employee with forwarded username, return false
            if (list.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// This method checks if forwarded email unique.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool UniqueEmail(string email)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            var list = employeeList.Where(x => x.Email == email).ToList();
            //if exists employee with forwarded email, return false
            if (list.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidationForReportsNumberPerDay(int employeeID, DateTime date)
        {
            Reports reports = new Reports();
            List<vwReport> reportList = reports.GetReports();
            List<vwReport> reportsOfEmployee = reportList.Where(x => x.EmployeeID == employeeID && x.Date == date).ToList();
            if (reportsOfEmployee.Count() == 2)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool ValidationForHours(int employeeID, int reportID, DateTime date, int hours)
        {
            Reports reports = new Reports();
            List<vwReport> reportList = reports.GetReports();
            int reportHours = reportList.Where(x => x.EmployeeID == employeeID && x.Date == date && x.ReportID != reportID).Select(x => x.Hours).FirstOrDefault();
            if ((reportHours + hours) > 12)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool UniqueJMBG(string jmbg, string oldJmbg)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            if (jmbg != oldJmbg)
            {
                var list = employeeList.Where(x => x.JMBG == jmbg).ToList();
                //if exists employee with forwarded jmbg, return false
                if (list.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool UniqueBankAccount(string bankAccount, string oldBankAccount)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            if (bankAccount!=oldBankAccount)
            {
                var list = employeeList.Where(x => x.BankAccountNumber == bankAccount).ToList();
                //if exists employee with forwarded bank account, return false
                if (list.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool UniqueUsername(string username, string oldUsername)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            if (username != oldUsername)
            {
                var list = employeeList.Where(x => x.Username == username).ToList();
                //if exists employee with forwarded username, return false
                if (list.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool UniqueEmail(string email, string oldEmail)
        {
            Employees employees = new Employees();
            List<tblEmployee> employeeList = employees.GetAllEmployees();
            if (email != oldEmail)
            {
                var list = employeeList.Where(x => x.Email == email).ToList();
                //if exists employee with forwarded email, return false
                if (list.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}