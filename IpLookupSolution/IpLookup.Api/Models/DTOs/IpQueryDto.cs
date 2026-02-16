namespace IpLookup.Api.Models.DTOs
{
    public class IpQueryDto
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ISP { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ThreatLevel { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
