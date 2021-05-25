using System;

namespace Application.Reviews
{
    public class ReviewDto
    {
        public int OfferID { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Id { get; set; }
    }
}