using HotelReservationSystem.Domain.Common;
using System.Collections.Generic;


namespace HotelReservationSystem.Domain.Entities
{
    public class Offer : AuditableEntity
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public uint MaxGuests { get; set; }
        // relation
        public PreviewOfferFile OfferPreviewPicture { get; set; }
        // relation
        public List<OfferFile> Pictures { get; set; }
        // relation
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        // relation
        public virtual List<Room> Rooms { get; set;}
        // relation
        public virtual List<Reservation> Reservations { get; set; }
        // relation
        public virtual List<Review> Reviews { get; set; }
    }
}
