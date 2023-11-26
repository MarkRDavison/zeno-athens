namespace mark.davison.athens.api.Configuration;

public class AppSettings : IAppSettings
{
    public string SECTION => "ATHENS";

    public AuthAppSettings AUTH { get; set; } = new();
    public DatabaseAppSettings DATABASE { get; set; } = new();
    public bool PRODUCTION_MODE { get; set; }
}
