using HotelReservationSystem.Domain.Common;
using HotelReservationSystem.Domain.Enums;
using HotelReservationSystem.Domain;
using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Hotel : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
