using eCommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.SignalR
{
    public static class HubRegistiration
    {
        /// <summary>
        /// Oluşturulacak tüm hub lar bu statik class  üzerinden maplenip endpoint verilecektir.
        /// </summary>
        /// <param name="webApplication"></param>
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/product-hub");
        }
    }
}
