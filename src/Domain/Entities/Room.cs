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
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
