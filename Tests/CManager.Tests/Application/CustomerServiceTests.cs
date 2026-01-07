using NSubstitute;
using CManager.Infrastructure.Repos;
using CManager.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CManager.Tests.Application;

public class CustomerServiceTests
{
    [Fact]
    public void GetCustomerByEmail_ReturnCustomerWhenCustomerFound()
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


        //act



        //assert
    }
}

