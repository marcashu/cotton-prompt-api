using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class ReportRequest
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public bool IsRedraw { get; set; }
    }
}
