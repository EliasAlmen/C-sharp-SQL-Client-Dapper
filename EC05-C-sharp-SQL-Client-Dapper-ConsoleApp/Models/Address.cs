﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC05_C_sharp_SQL_Client_Dapper_ConsoleApp.Models
{
    internal class Address
    {
        public string StreetName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;  
        public string City { get; set; } = string.Empty;    
    }
}
