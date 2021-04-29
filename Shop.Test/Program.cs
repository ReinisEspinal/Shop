using System;
using Microsoft.Extensions.Configuration;
using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Repository;
using Shop.Production.Api.Infrastructure.Repository.Contracts;

namespace Shop.Test
{
    class Program
    {

        static void Main(string[] args)
        {
          /*  using (ProductionContext db = new ProductionContext("Server=MSI\\SQLEXPRESS01;Database=SHOP;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                var repSupplier = new SupplierRepository(db);

                var suppliers= repSupplier.GetSuppliers();

                foreach (var supplier in suppliers)
                {
                    Console.WriteLine(supplier.Companyname);
                }
            }

            */
        }
    }
}
