using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetAvailableAsArtistOrdersRequest
    {

        [FromQuery(Name = "artistId")]
        public Guid ArtistId { get; set; }

        [FromQuery(Name = "priority")]
        public bool? Priority { get; set; }

        [FromQuery(Name = "changeRequest")]
        public bool ChangeRequest { get; set; }
    }
}
