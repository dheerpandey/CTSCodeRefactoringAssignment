using System;

namespace App
{
    public interface ICustomerCreditServiceWrapper
    {
        int GetCreditLimit(Customer customer);
    }

   public class CustomerCreditServiceWrapper : ICustomerCreditServiceWrapper
    {
        public int GetCreditLimit(Customer customer)
        {
            using (var customerCreditService = new CustomerCreditServiceClient())
            {
                return customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
            }
        }
    }
}