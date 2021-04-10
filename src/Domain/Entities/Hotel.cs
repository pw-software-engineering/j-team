using HotelReservationSystem.Domain.Common;
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
<<<<<<< HEAD

=======
>>>>>>> dcd930dda33e06e1cf4eb88be734ab41c5f5608e
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? HotelPreviewPictureId { get; set; }
        // relation
        public File HotelPreviewPicture { get; set; }
        // relation
        public List<File> Pictures { get; set; }
        // relation
        public virtual List<Offer> Offers { get; set; }
    }
}
