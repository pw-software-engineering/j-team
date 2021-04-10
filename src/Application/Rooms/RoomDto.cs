using HotelReservationSystem.Domain.Entities;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Application.Rooms
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string HotelRoomNumber { get; set; }
        public static int OfferToInt(Offer offer)
        {
            return offer.OfferId;
        }
        public List<int> OfferID => Offers.ConvertAll(new System.Converter<Offer, int>(OfferToInt));
        [JsonIgnore]
        public List<Offer> Offers { get; set; }
    }
}