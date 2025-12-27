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
            Console.WriteLine("3. Search Customer");
            Console.WriteLine("4. Delete Customer");
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
                    FindCustomerByEmail();
                    break;

                case "4":
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
            Console.WriteLine("Customer created");
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


    private void FindCustomerByEmail()
    {
        Console.Clear();

        // Code Row 121 is AI Generated
        var email = InputHelper.ValidateInput("Enter Email To Find Customer", ValidationType.Email);

        var customer = _customerService.GetCustomerByEmail(email, out bool hasError);

        if (hasError)
        {
            Console.Clear();
            Console.WriteLine("Something went wrong, please try again later");
        }

        if (customer == null)
        {
            Console.Clear();
            Console.WriteLine("Customer not found");
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.PhoneNumber}");
            Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
            Console.WriteLine($"ID: {customer.Id}");
        }

        OutputDialog("Press any key to continue...");
    }

    
    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("Delete Customer");

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

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
            while (true)
            {
                Console.Clear();

                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    Console.WriteLine($"[{i + 1}] {customer.FirstName} {customer.LastName} {customer.Email}");
                }

                Console.WriteLine("[0] Go back to menu");
                Console.WriteLine("Enter customer number you wish to delete: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.Clear();
                    OutputDialog("Not a valid number! Press any key to try again...");
                    continue;
                }

                if (choice == 0)
                {
                    return;
                }

                if (choice > customers.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                var index = choice - 1;
                var selectedCustomer = customers[index];


                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("You have selected: ");
                    Console.WriteLine($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");
                    Console.WriteLine($"Email: {selectedCustomer.Email}");
                    Console.WriteLine("Are you sure you want to delete this customer? ([y]=Yes / [n]=No)");
                    var confirmation = Console.ReadLine()!.ToLower();

                    if (confirmation == "y")
                    {
                        Console.Clear();
                        Console.WriteLine($"Customer Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");
                        Console.WriteLine($"Customer Email: {selectedCustomer.Email}");
                        Console.Write("Enter email address to delete customer: ");
                        var deleteInput = Console.ReadLine();

                        if (deleteInput == selectedCustomer.Email)
                        {
                            var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                            if (result)
                            {
                                OutputDialog("Customer was removed, press any key to go back...");
                                return;
                            }
                            else
                            {
                                Console.Clear();
                                OutputDialog("Something went wrong, please contact support. Press any key to continue...");
                                return;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            OutputDialog("Email did not match, Press any key to try again.");
                            break;
                        }
                    }
                    else if (confirmation == "n")
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        OutputDialog("Please enter 'y' for Yes or 'n' for No. Press any key to continue...");
                        continue;
                    }
                }
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