using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class SubmitDesignRequest
    {
        [Required]
        public string Design { get; set; }

        [Required]
        public string FileName { get; set; }
    }
}
