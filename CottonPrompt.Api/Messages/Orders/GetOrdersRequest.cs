using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetOrdersRequest
    {
        [Required]
        [FromQuery(Name = "priority")]
        public bool Priority { get; set; }

        [FromQuery(Name = "availableForArtists")]
        public bool AvailableForArtists { get; set; }

        [FromQuery(Name = "artistId")]
        public Guid? ArtistId { get; set; }

        [FromQuery(Name = "availableForCheckers")]
        public bool AvailableForCheckers { get; set; }

        [FromQuery(Name = "checkerId")]
        public Guid? CheckerId { get; set; }
    }
}
