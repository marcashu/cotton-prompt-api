namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record Order(
        int Id, 
        string Number, 
        bool IsPriority,
        string Concept,
        string PrintColor,
        decimal DesignBracket,
        IEnumerable<string> ImageReferences
    );
}
