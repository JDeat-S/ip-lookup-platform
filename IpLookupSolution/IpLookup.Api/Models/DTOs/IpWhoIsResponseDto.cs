using System.Text.Json.Serialization;

public class IpWhoIsResponseDto
{
    public bool Success { get; set; }

    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    public string Country { get; set; }
    public string City { get; set; }

    [JsonPropertyName("latitude")]
    public decimal Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public decimal Longitude { get; set; }

    public ConnectionDto Connection { get; set; }
}

public class ConnectionDto
{
    public string Isp { get; set; }
}
