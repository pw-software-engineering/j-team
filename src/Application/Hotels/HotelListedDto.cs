using HotelReservationSystem.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Hotels
{
    public class HotelListedDto
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public byte[] HotelPreviewPicture => HotelPreviewPictureData?.Data;
        [JsonIgnore]
        public File HotelPreviewPictureData { get; set; }
    }
}