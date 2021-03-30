using HotelReservationSystem.Domain.Common;
using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string content { get; set; }
        public int rating { get; set; }
        public DateTime creationDate { get; set; }
        public string reviewerUsername { get; set; }
    }
}
