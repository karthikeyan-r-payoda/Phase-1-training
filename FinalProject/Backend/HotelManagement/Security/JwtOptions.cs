namespace HotelManagement.Security;
public class JwtOptions
{
    public string Issuer { get; set; }

    public string Audience { get; set; }
    public string key { get; set; }
    public string ExpireMinutes { get; set; }
}
