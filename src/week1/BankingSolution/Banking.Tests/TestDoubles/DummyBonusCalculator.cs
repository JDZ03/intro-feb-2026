using Banking.Domain;


namespace Banking.Tests.TestDoubles;

public class DummyBonusCalculator : ICalculateBonusesForAccounts
{
    public decimal CalculateBonusForDeposit(decimal currentBalance, decimal depositAmount)
    {
        return 0;
    }
}

public class StubbedBonusCalculator : ICalculateBonusesForAccounts
{
    public decimal CalculateBonusForDeposit(decimal currentBalanace, decimal depositAmount)
    {
        if (currentBalanace == 5000M && depositAmount == 420.69M)
        {
            return 19M;
        } else
        {
            return 0;
        }
    }
}
