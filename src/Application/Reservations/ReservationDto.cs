using HotelReservationSystem.Domain.Entities;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;

namespace Application.Reservations
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public string HotelRoomNumber { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int ChildrenCount { get; set; }
        public int AdultsCount { get; set; }
        public int? RoomId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}