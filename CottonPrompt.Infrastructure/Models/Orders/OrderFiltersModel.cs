namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record OrderFiltersModel(
        IEnumerable<string> OrderNumbers,
        IEnumerable<string> Priorities,
        IEnumerable<Guid> Artists,
        IEnumerable<Guid> Checkers,
        IEnumerable<string> Customers,
        IEnumerable<string> Status,
        IEnumerable<int> UserGroups
    );
}
