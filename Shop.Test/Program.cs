using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shop.Production.Api.Infrastructure.Context;
using Shop.Production.Api.Infrastructure.Data.Entities;
using Shop.Production.Api.Infrastructure.Repository;
using Shop.Production.Api.Infrastructure.Repository.Contracts;

namespace Shop.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ProductionContext db = new ProductionContext("Server=MSI\\SQLEXPRESS01;Database=SHOP;Trusted_Connection=True;MultipleActiveResultSets=true"))
            //{

                //        var newProduct = new Product()
                //        {
                //            ProductId = 1,
                //            ProductName = "Product HHYDP",
                //            SupplierId = 1,
                //            CategoryId = 1,
                //            UnitPrice = 92121,
                //            Discontinued = false

                //        };

                //        _ProductRepository.Update(newProduct);
                //        await _ProductRepository.Commit();
                //        // Console.WriteLine(x.UnitPrice);
                //        Console.ReadLine();

                //  }
            }
        }
    }
