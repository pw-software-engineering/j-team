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
        public string AccessToken { get; set; }
        public string Password { get; set; }
        // relation
        public virtual List<Reservation> Reservations { get; set; }
        // relation
        public virtual List<Review> Reviews { get; set; }

    }
}
