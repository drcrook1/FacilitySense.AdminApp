
namespace FacilitySense.DataModels
{
    public class Rating
    {
        public int ID { get; set; }
        public int Stars { get; set; }
        public string? Comment { get; set; }
        public DateTime RatingDate { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public int FacilityID { get; set; }
        public Facility? Facility { get; set; }
    }
}
