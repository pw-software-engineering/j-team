using System.Collections.Generic;

namespace Application.Offers
{
    public class OfferDto
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] OfferPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public uint MaxGuests { get; set; }
    }
}