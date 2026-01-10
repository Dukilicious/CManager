using NSubstitute;
using CManager.Infrastructure.Repos;
using CManager.Domain.Models;
using System.ComponentModel.DataAnnotations;
using CManager.Application.Services;
using NSubstitute.Routing.Handlers;
using NSubstitute.ExceptionExtensions;

namespace CManager.Tests.Application;

public class CustomerServiceTests
{
    [Fact]
    public void GetCustomerByEmail_ReturnCustomer_WhenCustomerFound()
    {
        //arrange
        var customerRepo = Substitute.For<ICustomerRepo>();

        var customerList = new List<CustomerModel>
        {
            new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Donkey",
                LastName = "Kong",
                Email = "donkey@domain.se",
                PhoneNumber = "123456789",

                Address = new AddressModel
                {
                    StreetAddress = "MonkeyStreet 16",
                    PostalCode = "12345",
                    City = "Nintendo"
                }
            }
        };

        customerRepo.GetAllCustomers().Returns(customerList);

        
        
        
        //act
        var customerService = new CustomerService(customerRepo);

        //Code below (line 47) is AI generated. - Calls the method and stores the returned customer in a variable, also getting the hasError flag.
        var result = customerService.GetCustomerByEmail("DONkey@domAIn.se", out bool hasError);

        
        
        
        //assert
        Assert.False(hasError);
        Assert.NotNull(result);
        //Code below (line 56) is AI generated. - Verifies the returned customers email is the one i expect.
        Assert.Equal("donkey@domain.se", result.Email);
    }



    [Fact]
    public void GetCustomerByEmail_ReturnNull_WhenRepositoryThrows()
    {
        //arrange
        var customerRepo = Substitute.For<ICustomerRepo>();

        customerRepo.GetAllCustomers().Throws(new Exception("Test Error"));


        
        
        //act
        var customerService = new CustomerService(customerRepo);
        var result = customerService.GetCustomerByEmail("felEmail@domain.", out bool hasError);


        
        //assert
        Assert.True(hasError);
        Assert.Null(result);
    }
}