public class ServiceUrls
{
    public string BaseUrl { get; set; } = default!;

    public string LoginEndpoint =>
        $"{BaseUrl}/login";

    public string RegisterEndpoint =>
        $"{BaseUrl}/register";

    public string RefreshTokenEndpoint =>
        $"{BaseUrl}/register";

    public string CreateShortUrl =>
        $"{BaseUrl}/create";

    public string GetRedirectToUrl =>
        $"{BaseUrl}/";
}