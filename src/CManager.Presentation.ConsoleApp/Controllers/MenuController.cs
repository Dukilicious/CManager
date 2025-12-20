using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. Delete Customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose Option: ");


            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "3":
                    DeleteCustomer();
                    break;
                
                case "0":
                    return;

                default:
                    OutputDialog("Invalid Option! Press any key to continue...");
                    break;
            }
        }
    }


    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");

        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Street address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("Postal code", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("Customer created")
            Console.WriteLine($"{firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong, please try again.");
        }
        OutputDialog("Press any key to continue...");
    }


    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All customers");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong, please try again later");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");
                Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue...");
    }


    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}