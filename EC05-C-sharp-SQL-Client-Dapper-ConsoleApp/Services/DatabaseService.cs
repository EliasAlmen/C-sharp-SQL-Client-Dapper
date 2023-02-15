using Dapper;
using EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Models;
using EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Models.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Services
{
    internal class DatabaseService
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Elias\Downloads\EC-utbildning-webbutvecklare-NET\05-Datalagring\EC05-Databases\EC05-C-sharp-SQL-Client-Dapper\EC05-C-sharp-SQL-Client-Dapper-ConsoleApp\Data\local_sql_db.mdf;Integrated Security=True;Connect Timeout=30";
    
        public async Task SaveCustomerAsync(CustomerEntity customerEntity)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync("IF NOT EXISTS (SELECT Id FROM Customers WHERE Email = @Email) INSERT INTO Customers VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressId)", customerEntity);
        }

        public async Task<int> GetOrSaveAddressAsync(AddressEntity addressEntity)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>("IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City) INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @PostalCode, @City) ELSE SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City", addressEntity);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Customer>("SELECT c.Id, c.FirstName, c.LastName, c.Email, PhoneNumber, a.StreetName, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id");
        }
        public async Task<Customer> GetCustomerAsync(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Customer>("SELECT c.Id, c.FirstName, c.LastName, c.Email, PhoneNumber, a.StreetName, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id WHERE c.Email = @Email", new { Email = email });
        }
    }
}
