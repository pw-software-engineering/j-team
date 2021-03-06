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
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AccessToken { get; set; }
        public string Password { get; set; }
        public int? HotelPreviewPictureId { get; set; }
        // relation
        public File HotelPreviewPicture { get; set; }
        // relation
        public List<File> Pictures { get; set; }
        // relation
        public virtual List<Offer> Offers { get; set; }
        // relation
        public virtual List<Room> Rooms { get; set; }
    }
}
