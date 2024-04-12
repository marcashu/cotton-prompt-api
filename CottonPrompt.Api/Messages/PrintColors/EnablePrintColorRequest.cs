using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.PrintColors
{
    public class EnablePrintColorRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
