

namespace StringCalculator;
public class CalculatorTests
{
    [Fact]
    public void EmptyStringReturnsZero()
    {
        var calculator = new Calculator();

        var result = calculator.Add("");

        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData("1",1)]
    [InlineData("212", 212)]
    public void SingleDigit(string input, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1,1",2)]
    [InlineData("2,1", 3)]
    public void TwoDigits(string input, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1,2", 3)]
    [InlineData("1,2,3", 6)]
    [InlineData("1,2,3,4,5,6,7,8,9", 45)]
    public void VariableDigits(string input, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1\n2", 3)]
    [InlineData("1\n2,3", 6)]
    public void VariableDelimeters(string input, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("//#\n1#2#3", 6)]
    [InlineData("//#\n1#2,3\n1", 7)]
    public void UserDelimeters(string input, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(input);
        Assert.Equal(expected, result);
    }
}
