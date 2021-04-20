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
        public List<int> OfferID => OffersData?.ConvertAll(new System.Converter<Offer, int>(OfferToInt));
        public bool Contains(int offerId)
        {
            foreach (var id in OfferID)
                if (id == offerId)
                    return true;
            return false;
        }
        [JsonIgnore]
        public List<Offer> OffersData { get; set; }
    }
}