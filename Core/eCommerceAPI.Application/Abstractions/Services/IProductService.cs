﻿using eCommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Abstractions.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
