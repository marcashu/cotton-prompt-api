namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record DownloadOrderModel(
        Stream Content,
        string ContentType,
        string FileName
    );
}
