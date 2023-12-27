using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetOrdersRequest
    {
        [Required]
        [FromQuery(Name = "priority")]
        public bool Priority { get; set; }

        [FromQuery(Name = "hasArtistFilter")]
        public bool HasArtistFilter { get; set; }

        [FromQuery(Name = "artistId")]
        public Guid? ArtistId { get; set; }

        [FromQuery(Name = "hasCheckerFilter")]
        public bool HasCheckerFilter { get; set; }

        [FromQuery(Name = "checkerId")]
        public Guid? CheckerId { get; set; }
    }
}
