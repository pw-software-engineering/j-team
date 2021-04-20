﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public string HotelRoomNumber { get; set; }
        // relation 
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        // relation
        public virtual List<Offer> Offers { get; set; }
        public bool Contains(int offerId)
        {
            foreach (var offer in Offers)
                if (offer.OfferId == offerId)
                    return true;
            return false;
        }

    }
}
