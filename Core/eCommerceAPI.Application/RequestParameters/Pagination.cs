using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.RequestParameters
{
    public class Pagination
    {
        public int Page { get; set; } = 0; //default değer
        public int Size { get; set; } = 5; //default değer
    }
}
