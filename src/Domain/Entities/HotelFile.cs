using HotelReservationSystem.Domain.Common;
using System.Collections.Generic;


namespace HotelReservationSystem.Domain.Entities
{
    public class HotelFile : AuditableEntity
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Description { get; set; }
        // relation
        public int HotelId { get; set; }
        public Hotel Hotel;
    }
}
