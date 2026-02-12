using Banking.Domain;

namespace Banking.Tests.MakingDeposits;

public class DepositsIncreaseBalance
{
    [Fact]
    public void Depositing()
    {
        var account = new Account();
        var openingBalance = account.GetBalance();
        var amountToDeposit = 123.23M;

        account.Deposit(amountToDeposit);

        Assert.Equal(openingBalance + amountToDeposit, account.GetBalance());
    }
}
