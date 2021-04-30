﻿using Shop.Production.Api.Infrastructure.Services.ServicesResult.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Production.Api.Infrastructure.Services.Contracts
{
   public interface ISupplierService
    {
        SupplierServiceResultCore GetSupplier();
    }
}