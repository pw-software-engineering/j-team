using HotelReservationSystem.Domain.Common;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Client : AuditableEntity
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
