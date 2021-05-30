using HotelReservationSystem.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelReservationSystem.Application.Hotels
{
    public class DetailedOfferDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public uint MaxGuests { get; set; }

        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public static byte[] FileToBytes(File file)
        {
            return file.Data;
        }

        public List<byte[]> OfferPictures => OfferPicturesData?.ConvertAll(new System.Converter<File, byte[]>(FileToBytes));
        [JsonIgnore]
        public List<File> OfferPicturesData { get; set; }


    }
}
