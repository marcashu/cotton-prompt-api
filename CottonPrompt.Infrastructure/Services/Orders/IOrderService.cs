using CottonPrompt.Infrastructure.Messages.Orders;
using CottonPrompt.Infrastructure.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAsync();

        Task CreateAsync(CreateOrderRequest request);
    }
}
