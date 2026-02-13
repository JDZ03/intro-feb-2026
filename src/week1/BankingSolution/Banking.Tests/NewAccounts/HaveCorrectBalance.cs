using Banking.Domain;
using Banking.Tests.TestDoubles;
using NSubstitute;
namespace Banking.Tests.NewAccounts;

public class HaveCorrectBalance
{
    [Fact]
    public void BalanceIsCorrect()
    {
        // WTCYWYH - Write the code you wish you had.
        var myAccount = new Account(Substitute.For<ICalculateBonusesForAccounts>());

        decimal openingBalance = myAccount.GetBalance();

        // Fails on the Assert.
        Assert.Equal(5000M, openingBalance);
    }
}

