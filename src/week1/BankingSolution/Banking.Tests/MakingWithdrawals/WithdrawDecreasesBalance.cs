using Banking.Domain;
using Banking.Tests.TestDoubles;

namespace Banking.Tests.MakingWithdrawals;

public class WithdrawDecreasesBalance
{
    [Fact]
    public void Withdrawing()
    {
        var account = new Account(new DummyBonusCalculator());
        var openingBalance = account.GetBalance();
        var amountToWithdraw = 123.23M;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact]
    public void OverdraftIsUnbound()
    {
        var account = new Account(new DummyBonusCalculator());
        var openingBalance = account.GetBalance();
        var amountToWithdraw = openingBalance * 2;
        Assert.Throws<InvalidTransactionAmountException>(() => account.Withdraw(amountToWithdraw));
    }

    [Fact]
    public void CanWithdrawFullBalance()
    {
        var account = new Account(new DummyBonusCalculator());
        account.Withdraw(account.GetBalance());

        Assert.Equal(0M, account.GetBalance());
    }

    [Fact]
    public void TransactionAmountsMustBeCorrect()
    {
        // Deposit and withdrawal only allow amounts that are > 0
        var account = new Account(new DummyBonusCalculator());
        var openingBalance = account.GetBalance();

        Assert.Throws<InvalidTransactionAmountException>(() => account.Withdraw(-1000M));
    }

}
