using System.Collections.Generic;
using System.Text.Json.Serialization;
using HotelReservationSystem.Domain.Entities;

namespace Application.Offers
{
    public class OfferDto
    {
        public int OfferID { get; set; }
        public string OfferTitle { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public uint MaxGuests { get; set; }
        public byte[] OfferPreviewPicture => OfferPreviewPictureData?.Data;
        [JsonIgnore]
        public File OfferPreviewPictureData { get; set; }
        public static byte[] FileToBytes(File file)
        {
            return file.Data;
        }
        public List<byte[]> Pictures => PicturesData?.ConvertAll(new System.Converter<File, byte[]>(FileToBytes));
        [JsonIgnore]
        public List<File> PicturesData { get; set; }
    }
}