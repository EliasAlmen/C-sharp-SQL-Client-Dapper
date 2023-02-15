using EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Models;
using EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Services
{
    internal class CustomerService
    {
        public static async Task SaveAsync(Customer customer)
        {
            var database = new DatabaseService();

            await database.SaveCustomerAsync(new CustomerEntity
            {
                FirstName= customer.FirstName,
                LastName= customer.LastName,
                Email= customer.Email,
                PhoneNumber= customer.PhoneNumber,
                AddressId = await database.GetOrSaveAddressAsync(new AddressEntity
                {
                    StreetName = customer.StreetName,
                    PostalCode = customer.PostalCode,
                    City = customer.City,
                })
            });
        }

        public static async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var database = new DatabaseService();
            return await database.GetAllCustomersAsync();
        }

        public static async Task<Customer> GetAsync(string email)
        {
            var database = new DatabaseService();
            return await database.GetCustomerAsync(email);
        }
    }
}
