using Banking.Domain;
using Banking.Tests.TestDoubles;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Tests;

public class DepositUsesBonusCalculator
{
    [Fact]
    public void IntegratesProperly()
    {
        var stubbedBonusCalculator = Substitute.For<ICalculateBonusesForAccounts>();
        var account = new Account(stubbedBonusCalculator);
        var openingBalance = account.GetBalance();
        var amountToDeposit = 420.69M;
        stubbedBonusCalculator.CalculateBonusForDeposit(openingBalance, amountToDeposit).Returns(19M);
        account.Deposit(amountToDeposit);
        Assert.Equal(openingBalance + amountToDeposit + 19M, account.GetBalance());
    }
}
