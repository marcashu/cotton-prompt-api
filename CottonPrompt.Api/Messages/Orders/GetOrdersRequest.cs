using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetOrdersRequest
    {
        [FromQuery(Name = "priority")]
        public bool? Priority { get; set; }

        [FromQuery(Name = "artistStatus")]
        public string? ArtistStatus { get; set; }

        [FromQuery(Name = "checkerStatus")]
        public string? CheckerStatus { get; set; }

        [FromQuery(Name = "noArtist")]
        public bool NoArtist { get; set; }

        [FromQuery(Name = "artistId")]
        public Guid? ArtistId { get; set; }

        [FromQuery(Name = "noChecker")]
        public bool NoChecker { get; set; }

        [FromQuery(Name = "checkerId")]
        public Guid? CheckerId { get; set; }
    }
}
