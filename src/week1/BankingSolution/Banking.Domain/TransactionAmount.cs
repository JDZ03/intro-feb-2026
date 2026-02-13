namespace Banking.Domain;

public class InvalidTransactionAmountException : ArgumentOutOfRangeException { }

public class TransactionAmount
{
    private decimal Value { get; }
    private TransactionAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidTransactionAmountException();
        }
        Value = amount;
    }

    public static TransactionAmount FromDecimal(decimal amt)
    {
        // do the check...
        return new TransactionAmount(amt);
    }

    public static TransactionAmount FromInt(int amt)
    {
        // do the check...
        return new TransactionAmount(amt);
    }

    public static TransactionAmount FromString(string amt)
    {
        // do the check...
        if (decimal.TryParse(amt, out decimal val))
        {
            return new TransactionAmount(val);
        }
        else
        {
            throw new InvalidTransactionAmountException();
        }
    }

    public static implicit operator Decimal(TransactionAmount amount) => amount.Value;
    public static implicit operator TransactionAmount(decimal value) => new(value);

}