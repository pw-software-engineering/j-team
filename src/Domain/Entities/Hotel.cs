﻿using HotelReservationSystem.Domain.Common;
using HotelReservationSystem.Domain.Enums;
using HotelReservationSystem.Domain;
using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Hotel : AuditableEntity
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public byte[] HotelPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // relation
        public List<Offer> Offers { get; set; }
    }
}
