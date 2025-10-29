using CleanShortener.Domain;

namespace CleanShortener.Tests
{
    public class ShortUrlTests
    {
        [Theory]
        [InlineData("http://www.google.com")]
        [InlineData("https://www.google.com")]
        public void HTTPBasedAbsoluteURLPassesValidation(string url)
        {
            // Act
            var validationResult = ShortUrl.Validate(url);

            // Assert
            validationResult.HasErrors.ShouldBeFalse();
            validationResult.Errors.ShouldBeEmpty();
        }

        [Fact]
        public void ValidationFailsWhenURLIsEmpty()
        {
            // Arrange
            const string Url = "";

            // Act
            var validationResult = ShortUrl.Validate(Url);

            // Assert
            validationResult.HasErrors.ShouldBeTrue();
            validationResult.Errors.Count.ShouldBe(3);
        }

        [Fact]
        public void NullURLIsHandledGracefullyAndValidationFails()
        {
            // Arrange
            const string Url = null!;

            // Act
            var validationResult = ShortUrl.Validate(Url);

            // Assert
            validationResult.HasErrors.ShouldBeTrue();
            validationResult.Errors.Count.ShouldBe(3);
        }

        [Fact]
        public void ValidationFailsForNonHttpBasedProtocols()
        {
            // Arrange
            const string Url = "fpt://127.0.0.1";

            // Act
            var validationResult = ShortUrl.Validate(Url);

            // Assert
            validationResult.HasErrors.ShouldBeTrue();
            validationResult.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public void ValidationFailsForNonAbsoluteURL()
        {
            // Arrange
            const string Url = "www.google.com";

            // Act
            var validationResult = ShortUrl.Validate(Url);

            // Assert
            validationResult.HasErrors.ShouldBeTrue();
            validationResult.Errors.Count.ShouldBe(2);
        }
    }
}