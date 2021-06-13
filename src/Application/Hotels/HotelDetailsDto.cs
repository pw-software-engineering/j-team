using HotelReservationSystem.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Hotels
{
    public class HotelDetailsDto
    {
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public static byte[] FileToBytes(File file)
        {
            return file.Data;
        }
        public List<byte[]> HotelPictures => HotelPicturesData?.ConvertAll(new System.Converter<File, byte[]>(FileToBytes));
        [JsonIgnore]
        public List<File> HotelPicturesData { get; set; }
    }
}