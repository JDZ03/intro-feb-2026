namespace Banking.Domain;

public class Account(ICalculateBonusesForAccounts _bonusCalculator)
{
    private decimal _currentBalance = 5000M;


    public virtual void Deposit(TransactionAmount amountToDeposit)
    {
        _currentBalance += amountToDeposit + _bonusCalculator.CalculateBonusForDeposit(_currentBalance, amountToDeposit);
    }

    public decimal GetBalance()
    {

        return _currentBalance;
    }

    // Primitive Obsession 
    public void Withdraw(TransactionAmount amountToWithdraw)
    {
        if (WouldCauseOverdraft(amountToWithdraw))
        {
            throw new InvalidTransactionAmountException();
        }
        _currentBalance -= amountToWithdraw;
    }

    private bool WouldCauseOverdraft(decimal amountToWithdraw)
    {
        return amountToWithdraw > _currentBalance;
    }

    public class OverdraftNotAllowedException : ArgumentOutOfRangeException { }

}

public interface ICalculateBonusesForAccounts
{
    decimal CalculateBonusForDeposit(decimal currentBalance, decimal depositAmount);
}