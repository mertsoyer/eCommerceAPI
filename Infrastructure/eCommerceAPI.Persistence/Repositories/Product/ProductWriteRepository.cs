﻿using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Domain.Entities;
using eCommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(eCommerceAPIDbContext context) : base(context)
        {
        }
    }
}
