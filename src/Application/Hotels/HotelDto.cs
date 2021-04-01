using System.Collections.Generic;

namespace Application.Hotels
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        // public byte[] HotelPreviewPicture { get; set; }
        // public List<byte[]> Pictures { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}