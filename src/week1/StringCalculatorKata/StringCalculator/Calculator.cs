
public class Calculator
{
    public int Add(string numbers)
    {
        return numbers == "" ? 0
            : numbers.Length > 3 && numbers.Substring(0, 2).Equals("//") ? 
                 numbers.Substring(numbers.IndexOf('\n') + 1)
                 .Split([',', '\n', numbers[2]])
                 .Select(int.Parse)
                 .Sum()
            : numbers.Split([',', '\n'])
                 .Select(int.Parse)
                 .Sum();
    }
}
