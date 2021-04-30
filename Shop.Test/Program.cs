using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Repository;
using Shop.Production.Api.Infrastructure.Repository.Contracts;

namespace Shop.Test
{
    class Program
    {

        static async Task Main(string[] args)
        {
            using (ProductionContext db = new ProductionContext(@"Server=MSI\\SQLEXPRESS01;Database=SHOP;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                var x = new ProductRepository(db);

               var b=  await x.GetById(1);

                Console.WriteLine(b.ProductId);
            }
                    //{
            //    var repSupplier = new SupplierRepository(db);

            //    var suppliers = repSupplier.GetSuppliers();

            //    foreach (var supplier in suppliers)
            //    {
            //        Console.WriteLine(supplier.Companyname);
            //    }
            //}


        }
    }
}
