using Banking.Domain;

namespace Banking.Tests.MakingWithdrawals;

public class WithdrawDecreasesBalance
{
    [Fact]
    public void Withdrawing()
    {
        var account = new Account();
        var openingBalance = account.GetBalance();
        var amountToWithdraw = 123.23M;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact]
    public void Overdraft()
    {
        var account = new Account();
        var openingBalance = account.GetBalance();
        var amountToWithdraw = openingBalance * 2;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance, account.GetBalance());
    }

    [Fact]
    public void CanWithdrawFullBalance()
    {
        var account = new Account();
        account.Withdraw(account.GetBalance());

        Assert.Equal(0M, account.GetBalance());
    }

    [Fact]
    public void TransactionAmountsMustBeCorrect()
    {
        // Deposit and withdrawal only allow amounts that are > 0
        var account = new Account();
        var openingBalance = account.GetBalance();
        var amountToWithdraw = -1000M;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

}
