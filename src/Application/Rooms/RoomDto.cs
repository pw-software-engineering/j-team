using HotelReservationSystem.Domain.Entities;
using System.Collections.Generic;

namespace Application.Rooms
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string HotelRoomNumber { get; set; }
        // public List<int> OfferId { get; set; }
    }
}