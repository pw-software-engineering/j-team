using HotelReservationSystem.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Hotels
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        // public List<byte[]> Pictures { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public byte[] HotelPreviewPicture => HotelPreviewPictureData?.Data;
        [JsonIgnore]
        public File HotelPreviewPictureData { get; set; }
    }
}