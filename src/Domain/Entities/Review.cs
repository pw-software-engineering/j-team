using HotelReservationSystem.Domain.Common;
using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        // relation
        public int ClientId { get; set; }
        public Client Client { get; set; }
        // relation
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
