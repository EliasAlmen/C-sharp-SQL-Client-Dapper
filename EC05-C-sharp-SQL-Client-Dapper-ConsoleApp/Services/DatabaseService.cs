using EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Models;
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
    
    

        public void SaveCustomer(Customer customer)
        {

        }


    }
}
