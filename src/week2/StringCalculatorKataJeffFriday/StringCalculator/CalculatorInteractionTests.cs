namespace StringCalculator;

public class CalculatorInteractionTests
{
    [Theory]
    [InlineData("1,2", "3")]
    [InlineData("42", "42")]
    public void TheLoggerLogs(string example, string expected)
    {

        var mockedLogger = Substitute.For<ILogger>();
        var mockedNotifier = Substitute.For<INotifyTheHelpDesk>();
        var calculator = new Calculator(mockedLogger, mockedNotifier);
        var result = calculator.Add(example);

        // This test will only pass if 
        // "3" is written to the logger
        mockedLogger.Received().LogAddResults(expected);
    }

    [Theory]
    [InlineData("1,2", "3")]
    public void LoggerThrows(string example, string expected)
    {

        var stubbedLogger = Substitute.For<ILogger>();
        var mockedNotifier = Substitute.For<INotifyTheHelpDesk>();
        var calculator = new Calculator(stubbedLogger, mockedNotifier);
        stubbedLogger.When(logger => logger.LogAddResults(Arg.Any<string>()))
            .Throw(new Exception("Blammo~"));

        var result = calculator.Add(example);

        mockedNotifier.Received(1).Notify("Wasn't able to log: 3");
    }

    [Theory]
    [InlineData("1,2", "3")]
    public void HelpDeskNotnotifiedWithNoException(string example, string expected)
    {

        var stubbedLogger = Substitute.For<ILogger>();
        var mockedNotifier = Substitute.For<INotifyTheHelpDesk>();
        var calculator = new Calculator(stubbedLogger, mockedNotifier);

        var result = calculator.Add(example);

        mockedNotifier.DidNotReceive()
            .Notify(Arg.Any<string>());
    }
}
