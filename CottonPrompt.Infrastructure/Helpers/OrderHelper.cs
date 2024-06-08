using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Helpers
{
    public static class OrderHelper
    {
        public static IQueryable<Order> FilterOrders(IQueryable<Order> queryableOrders, OrderFiltersModel? filters)
        {
            if (filters != null)
            {
                if (filters.OrderNumbers.Any())
                {
                    queryableOrders = queryableOrders.Where(o => filters.OrderNumbers.Contains(o.OrderNumber));
                }

                if (filters.Priorities.Count() == 1)
                {
                    var priority = filters.Priorities.First() == "Yes";
                    queryableOrders = queryableOrders.Where(o => o.Priority == priority);
                }

                if (filters.Artists.Any())
                {
                    queryableOrders = queryableOrders.Where(o => o.ArtistId != null && filters.Artists.Any(a => a == o.ArtistId));
                }

                if (filters.Checkers.Any())
                {
                    queryableOrders = queryableOrders.Where(o => o.CheckerId != null && filters.Checkers.Any(a => a == o.CheckerId));
                }

                if (filters.Customers.Any())
                {
                    queryableOrders = queryableOrders.Where(o => filters.Customers.Contains(o.CustomerEmail));
                }

                var artistStatuses = filters.Status.Where(s => s.StartsWith("ArtistStatus")).Select(s => s.Replace("ArtistStatus-", ""));
                if (artistStatuses.Any())
                {
                    queryableOrders = queryableOrders.Where(o => artistStatuses.Contains(o.ArtistStatus));
                }

                var checkerStatuses = filters.Status.Where(s => s.StartsWith("CheckerStatus")).Select(s => s.Replace("CheckerStatus-", ""));
                if (checkerStatuses.Any())
                {
                    queryableOrders = queryableOrders.Where(o => checkerStatuses.Contains(o.CheckerStatus));
                }

                if (filters.UserGroups.Any())
                {
                    queryableOrders = queryableOrders.Where(o => filters.UserGroups.Contains(o.UserGroupId));
                }
            }

            return queryableOrders;
        }
    }
}
