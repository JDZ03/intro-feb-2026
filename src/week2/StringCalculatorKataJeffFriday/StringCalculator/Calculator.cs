using NSubstitute.Core;


public interface ILogger
{
    void LogAddResults(string results);
}


public interface INotifyTheHelpDesk
{
    void Notify(string not);
}
public class Calculator(ILogger _logger, INotifyTheHelpDesk _helpDesk)
{
    public int Add(string numbers)
    {
        var result = numbers == "" ? 0 :  numbers // "1,2,3,4"
                .Split(',', '\n') // ["1", "2", "3", "4"]
                .Select(int.Parse) // [1, 2, 3, 4]
                .Sum(); // 10

        try
        {
            _logger.LogAddResults(result.ToString());
        }
        catch (Exception)
        {

            _helpDesk.Notify("Wasn't able to log: " + result.ToString());
            // gulp!
        }
        return result; 
        }
}

// Test Double
// Dummy - not really part of the test, just need something so we don't get a NRE
// Stub - a thing that has canned responses to questions. Simulating faults. 
// Mock - record their interactions. 
// Fake - We will do this in our API. It's not our codem it's a "stand in" for something
// That will be there in 

