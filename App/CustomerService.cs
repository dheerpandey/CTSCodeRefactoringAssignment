using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService
    {
        ICustomerCreditServiceWrapper _CustomerCreditServiceWrapper = null;

       public CustomerService(ICustomerCreditServiceWrapper customerCreditServiceWrapper)
        {

            _CustomerCreditServiceWrapper = customerCreditServiceWrapper; 
        }

        public bool AddCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname) || customer.Company == null)
            {
                return false;
            }

            if (!customer.EmailAddress.Contains("@") && !customer.EmailAddress.Contains("."))
            {
                return false;
            }

            int age = calculateAge(customer);
            if (age < 21)
            {
                return false;
            }

            customer.HasCreditLimit = true;
            if (customer.Company.Name == "VeryImportantClient")
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (customer.Company.Name == "ImportantClient")
            {

                // Do credit check and double credit limit                
                customer.CreditLimit = _CustomerCreditServiceWrapper.GetCreditLimit(customer) *2;
            }
            else
            {
                // Do credit check
                customer.CreditLimit = _CustomerCreditServiceWrapper.GetCreditLimit(customer);
            }

            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            CustomerDataAccess.AddCustomer(customer);

            return true;
        }

        private static int calculateAge(Customer customer)
        {
            var now = DateTime.Now;
            int age = now.Year - customer.DateOfBirth.Year;
            if (now.Month < customer.DateOfBirth.Month || (now.Month == customer.DateOfBirth.Month && now.Day < customer.DateOfBirth.Day)) age--;
            return age;
        }
    }
}
