using CleanShortener.Domain;

namespace CleanShortener.Tests;

public class ShortUrlTests
{

    private const string ShortenerBaseUrl = "http://short.url";

    [Theory]
    [InlineData("http://www.google.com")]
    [InlineData("https://www.google.com")]
    public void HTTPBasedAbsoluteURLPassesValidation(string url)
    {
        // Act
        var urlResult = ShortUrl.TryCreate(url, ShortenerBaseUrl);

        // Assert
        urlResult.IsSuccess.ShouldBeTrue();
        urlResult.ValidationErrors.ShouldBeNull();
    }

    [Fact]
    public void ValidUrlIsShortenedToInternalUrlWithUniqueIdentifier()
    {
        // Arrange
        const string Url = "http://www.google.com";

        // Act
        var urlResult = ShortUrl.TryCreate(Url, ShortenerBaseUrl);

        // Assert
        urlResult.IsSuccess.ShouldBeTrue();
        urlResult.Entity!.ShortenedUrl.ShouldStartWith(ShortenerBaseUrl);
    }

    [Fact]
    public void ValidationFailsWhenURLIsEmpty()
    {
        // Arrange
        const string Url = "";

        // Act
        var urlResult = ShortUrl.TryCreate(Url, ShortenerBaseUrl);

        // Assert
        urlResult.IsFailure.ShouldBeTrue();
        urlResult.ValidationErrors!.Errors.Count.ShouldBe(1);
    }

    [Fact]
    public void NullURLIsHandledGracefullyAndValidationFails()
    {
        // Arrange
        const string Url = null!;

        // Act
        var urlResult = ShortUrl.TryCreate(Url, ShortenerBaseUrl);

        // Assert
        urlResult.IsFailure.ShouldBeTrue();
        urlResult.ValidationErrors!.Errors.Count.ShouldBe(1);
    }

    [Fact]
    public void ValidationFailsForNonHttpBasedProtocols()
    {
        // Arrange
        const string Url = "fpt://127.0.0.1";

        // Act
        var urlResult = ShortUrl.TryCreate(Url, ShortenerBaseUrl);

        // Assert
        urlResult.IsFailure.ShouldBeTrue();
        urlResult.ValidationErrors!.Errors.Count.ShouldBe(1);
    }

    [Fact]
    public void ValidationFailsForNonAbsoluteURL()
    {
        // Arrange
        const string Url = "www.google.com";

        // Act
        var urlResult = ShortUrl.TryCreate(Url, ShortenerBaseUrl);

        // Assert
        urlResult.IsFailure.ShouldBeTrue();
        urlResult.ValidationErrors!.Errors.Count.ShouldBe(2);
    }

    [Fact]
    public void MissingInternalUrlInvariantThrows()
    {
        // Arrange
        const string Url = "http://www.google.com";

        // Act & Assert
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = ShortUrl.TryCreate(Url, null!);
        });
    }
}