using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Api.DataStructures;
using static Nop.Plugin.Api.Infrastructure.Constants;

namespace Nop.Plugin.Api.Services
{
    public interface IShoppingCartItemApiService
    {
        Task<ApiList<ShoppingCartItem>> GetShoppingCartItems(int? customerId = null, DateTime? createdAtMin = null, DateTime? createdAtMax = null,
                                                    DateTime? updatedAtMin = null, DateTime? updatedAtMax = null, int limit = Configurations.DefaultLimit, 
                                                    int page = Configurations.DefaultPageValue);

        Task<ShoppingCartItem> GetShoppingCartItem(int id);
    }
}