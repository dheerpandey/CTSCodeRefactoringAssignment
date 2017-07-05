using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using App.Fakes;
using Microsoft.QualityTools.Testing.Fakes;

namespace App.Tests
{
    [TestClass()]
    public class CustomerServiceTests
    {
        Mock<ICustomerCreditServiceWrapper> _CustomerServiceMock = null;
        Mock<ICompanyRepository> _CompanyRepository = null;
       public CustomerServiceTests()
        {
           
        }


        [TestInitialize]
        public void Initialize()
        {

            _CustomerServiceMock = new Mock<ICustomerCreditServiceWrapper>();
            _CompanyRepository = new Mock<ICompanyRepository>();
            var _context = ShimsContext.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _CustomerServiceMock = null;
            ShimsContext.Reset();
        }

        [TestMethod()]
        public void AddCustomerTest()
        {
            _CustomerServiceMock.Setup(x => x.GetCreditLimit(It.IsAny<Customer>())).Returns(600);           

            CustomerService customerService = new CustomerService(_CustomerServiceMock.Object);

            var customerNoName = new Customer() { };

            ShimCustomerDataAccess.AddCustomerCustomer = (param) => { };

            Assert.IsFalse(customerService.AddCustomer(customerNoName), "Customer name should not be blank.");


            _CompanyRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                new Company()
                {
                    Id = 10,
                    Name = "ImportantClient",
                    Classification = Classification.Gold
                }
           );

            var companyService = new CompanyService(_CompanyRepository.Object);

            var customerValid = new Customer() { Firstname = "Ravi", Surname = "Shanker",
                EmailAddress = "ravi@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(30),
                Company = companyService.GetById(10)
            };

            Assert.IsFalse(customerService.AddCustomer(customerNoName), "Failed to add customer.");
        }
    }
}