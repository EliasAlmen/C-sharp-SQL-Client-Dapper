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

        public static async Task UpdateAsync(Customer customer)
        {
            var database = new DatabaseService();

            var customerEntity = await database.GetCustomerEntityByIdAsync(customer.Id);

            if (!string.IsNullOrWhiteSpace(customer.FirstName)) { customerEntity.FirstName= customer.FirstName; }
            if (!string.IsNullOrWhiteSpace(customer.LastName)) { customerEntity.LastName= customer.LastName; }
            if (!string.IsNullOrWhiteSpace(customer.Email)) { customerEntity.Email= customer.Email; }
            if (!string.IsNullOrWhiteSpace(customer.PhoneNumber)) { customerEntity.PhoneNumber= customer.PhoneNumber; }

            var addressEntity = await database.GetAddressEntityByIdAsync(customerEntity.AddressId);
            if (!string.IsNullOrWhiteSpace(customer.StreetName)) { addressEntity.StreetName= customer.StreetName; }
            if (!string.IsNullOrWhiteSpace(customer.PostalCode)) { addressEntity.PostalCode= customer.PostalCode; }
            if (!string.IsNullOrWhiteSpace(customer.City)) { addressEntity.City= customer.City; }

            customerEntity.AddressId = await database.GetOrSaveAddressAsync(addressEntity);

            await database.UpdateCustomerAsync(customerEntity);
        }

        public static async Task DeleteAsync(string email)
        {
            var database = new DatabaseService();
            await database.DeletCustomerAsync(email);
        }
    }
}
