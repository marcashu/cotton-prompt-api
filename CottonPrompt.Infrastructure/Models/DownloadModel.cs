namespace CottonPrompt.Infrastructure.Models
{
    public record DownloadModel(
        Stream Content,
        string ContentType,
        string FileName
    );
}
