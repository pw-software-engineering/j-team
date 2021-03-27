using HotelReservationSystem.Domain.Common;
using System.Collections.Generic;


namespace HotelReservationSystem.Domain.Entities
{
    public class Offer : AuditableEntity
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public byte[] OfferPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public uint MaxGuests { get; set; }
        // relation
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        // relation
        public List<Room> Rooms { get; set;}
    }
}
