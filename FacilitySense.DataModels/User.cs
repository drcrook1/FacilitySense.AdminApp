
namespace FacilitySense.DataModels
{
    public class User
    {
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Username { get; set; }

        public ICollection<Rating>? Ratings { get; set; }
    }
}
