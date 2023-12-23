namespace mark.davison.athens.bff.common;

public class AthensHttpRepository : HttpRepository
{
    public AthensHttpRepository(string baseUri, JsonSerializerOptions options) : base(baseUri, new HttpClient(), options)
    {

    }
    public AthensHttpRepository(string baseUri, HttpClient client, JsonSerializerOptions options) : base(baseUri, client, options)
    {

    }
}