﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Sale.Api.Infraestructure.Service.Models.Customers
{
    public class CustomersAddModel
    {
        public int CustId { get; set; }
        public string CompanyName { get; set; }
        public string Contacttitle { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public CustomersAddModel()
        {
            CreationDate = System.DateTime.Now;
        }
    }
}
