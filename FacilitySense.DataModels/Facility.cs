namespace FacilitySense.DataModels
{
    public class Facility
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public ICollection<Rating>? Ratings { get; set; }
    }
}