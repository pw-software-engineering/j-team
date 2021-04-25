using HotelReservationSystem.Domain.Common;
using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Reservation : AuditableEntity
    {
        public int ReservationId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int ChildrenCount { get; set; }
        public int AdultsCount { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        // relation 
        public int ClientId { get; set; }
        public Client Client { get; set; }
        // relation
        public int OfferId { get; set; }
        public Offer Offer { get; set; }

    }
}
