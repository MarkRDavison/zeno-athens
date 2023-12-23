namespace mark.davison.athens.web.ui;

public class AthensClientHttpRepository : ClientHttpRepository
{
    public AthensClientHttpRepository(
        string remoteEndpoint,
        IHttpClientFactory httpClientFactory
    ) : base(
        remoteEndpoint,
        httpClientFactory.CreateClient(WebConstants.ApiClientName))
    {
    }
}
