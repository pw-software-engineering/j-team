using HotelReservationSystem.Domain.Common;
using HotelReservationSystem.Domain.ValueObjects;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Offer : AuditableEntity
    {
        public int Id { get; set; }
    }
}
