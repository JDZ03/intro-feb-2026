namespace Banking.Domain;

public class StandardBonusCalculator : ICalculateBonusesForAccounts
{
    public decimal CalculateBonusForDeposit(decimal currentBalance, decimal depositAmount)
    {
        return currentBalance >= 5000M ? depositAmount * .10M : 0;
    }
}

public class SuperDeluxBonusCalculator
{
    public decimal CalculateBonusForDeposit(decimal currentBalance, decimal depositAmount)
    {
        return currentBalance >= 5000M ? depositAmount * .10M : 0;
    }
}

