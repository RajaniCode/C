namespace CS10.NET6FluentAssertions;

// dotnet add package FluentAssertions --version 6.12.0
using FluentAssertions;

public class UnitTestImprovedMethodGroupConversionToDelegate
{
    [Fact]
    public void Assertions()
    {

        var worldRecord = new Alphabet("Alpha testing", "Beta testing", "Gamma ray", "Delta Release",
            new Alphanumeric
            (
                Sigma: "Universal Set",
                Pi: 3.141592653589793D,
                Theta: 360,
                DateAndTime: DateTime.UtcNow,
                UUID: Guid.NewGuid()
            ));
        var trackRecord = new Alphabet("Alpha testing", "Beta testing", "100 MeV", "Release candidate",
             new Alphanumeric
            (
                Sigma: "Universal Set",
                Pi: 3.141592653589793D,
                Theta: 180,
                DateAndTime: DateTime.Now,
                UUID: Guid.NewGuid()
            ));

        worldRecord.Should().BeEquivalentTo(trackRecord, ExcludingCertainProperties);
        worldRecord.Letters.Should().BeEquivalentTo(trackRecord.Letters, ExcludingCertainProperties);
    }


    private FluentAssertions.Equivalency.EquivalencyAssertionOptions<Alphabet> ExcludingCertainProperties
        (FluentAssertions.Equivalency.EquivalencyAssertionOptions<Alphabet> _)
    {
        return _
            .Excluding(x => x.Gamma)
            .Excluding(x => x.Delta)
            .Excluding(x => x.Letters)
            .Using<DateTime>(x => x.Subject
                .Should()
                .BeCloseTo(x.Expectation, TimeSpan.FromHours(6)))
            .WhenTypeIs<DateTime>();
    }

    private FluentAssertions.Equivalency.EquivalencyAssertionOptions<Alphanumeric> ExcludingCertainProperties
        (FluentAssertions.Equivalency.EquivalencyAssertionOptions<Alphanumeric> _)
    {
        return _
            .Excluding(x => x.UUID)
            .Excluding(x => x.Theta)
            .Using<DateTime>(x => x.Subject
                .Should()
                .BeCloseTo(x.Expectation, TimeSpan.FromHours(6)))
            .WhenTypeIs<DateTime>();
    }
}

record Alphabet(string Alpha, string Beta, string Gamma, string Delta, Alphanumeric Letters);

record Alphanumeric(string Sigma, double Pi, int Theta, DateTime DateAndTime, Guid UUID);


// Credit:
/*
https://dotnet.microsoft.com/
*/