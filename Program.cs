using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace SdgBank
{
  class Program
  {
    static void Main(string[] args)
    {

      static string verifyAccount(string toCheck)
      {
        while (toCheck != "checking" && toCheck != "savings")
        {
          Console.Clear();
          Console.WriteLine("Please choose either (CHECKING) or (SAVINGS)");
          toCheck = Console.ReadLine().ToLower();
        }
        return toCheck;
      }

      static decimal verifyAmount(string toCheck)
      {

        decimal depositNumber;
        while (!decimal.TryParse(toCheck, out depositNumber))
        {
          Console.WriteLine("Please enter a valid money amount.  Example 13.45");
          toCheck = Console.ReadLine().ToLower();
        }
        return depositNumber;
      }

      var bankContext = new BankContext();
      var userName = "";

      //var userName = "";

      Console.Clear();
      Console.WriteLine("$$$  Welcome to SDG Bank.  $$$");
      Console.WriteLine("");

      //Can't get this to work

      bankContext.SeeAccounts();

      Console.WriteLine("Please select a user account.");
      Console.WriteLine("");
      userName = Console.ReadLine().ToLower();
      var validUser = false;
      validUser = bankContext.ValidateUser(userName);
      while (validUser == false)
      {
        Console.WriteLine("That is not a valid user.  Please try again.");
        userName = Console.ReadLine().ToLower();
        validUser = bankContext.ValidateUser(userName);
      }

      Console.WriteLine("Please enter your password.");
      var userPassword = Console.ReadLine().ToLower();
      var validPassword = false;
      validUser = bankContext.ValidatePassword(userPassword);
      while (validUser == false)
      {
        Console.WriteLine("That is not a valid password.  Please try again.");
        Console.WriteLine("For this project all passwords are 'password'.");
        userPassword = Console.ReadLine().ToLower();
        validUser = bankContext.ValidatePassword(userPassword);
      }

      var isRunning = true;
      while (isRunning)
      {

        bankContext.SeeTotals(userName);
        Console.WriteLine("");
        Console.WriteLine("What Would you like to do today?");
        Console.WriteLine("  (DEPOSIT) - Deposit money.");
        Console.WriteLine("  (WITHDRAWL) - Withdrawl money.");
        Console.WriteLine("  (TRANSFER) - Transfer money.");
        Console.WriteLine("  (BALANCE) - Check your balance.");
        Console.WriteLine("  (QUIT) - Leave the virtual bank.");
        var input = Console.ReadLine().ToLower();

        while (input != "deposit" && input != "withdrawl" && input != "transfer" && input != "balance" && input != "quit")
        {
          Console.Clear();
          Console.WriteLine("Please choose one of the following.");
          Console.WriteLine("  (DEPOSIT) - Deposit money.");
          Console.WriteLine("  (WITHDRAWL) - Withdrawl money.");
          Console.WriteLine("  (TRANSFER) - Transfer money.");
          Console.WriteLine("  (BALANCE) - Check your balance.");
          Console.WriteLine("  (QUIT) - Leave the virtual bank.");
          input = Console.ReadLine().ToLower();
        }

        switch (input)
        {
          case "deposit":
            Console.Clear();
            Console.WriteLine("What account do you want to deposit into (CHECKING) or (SAVINGS)");
            var depositAccountType = Console.ReadLine().ToLower();
            depositAccountType = verifyAccount(depositAccountType);

            Console.WriteLine("How much will you be depositing?");
            var depositAmount = Console.ReadLine().ToLower();
            decimal depositNumber = verifyAmount(depositAmount);

            Console.Clear();
            bankContext.MakeDeposit(depositAccountType, depositNumber, userName);
            Console.WriteLine($"I have deposited {depositNumber} into your {depositAccountType} account.");

            break;

          case "withdrawl":

            Console.Clear();
            Console.WriteLine("What account do you want to withdrawl from (CHECKING) or (SAVINGS)");
            var withdrawlAccountType = Console.ReadLine().ToLower();
            withdrawlAccountType = verifyAccount(withdrawlAccountType);

            Console.WriteLine("How much will you be withdrawling?");
            var withdrawlAmount = Console.ReadLine().ToLower();
            decimal withdrawlNumber = verifyAmount(withdrawlAmount);

            Console.Clear();
            bankContext.MakeWithdrawl(withdrawlAccountType, withdrawlNumber, userName);
            Console.WriteLine($"You withdrew {withdrawlNumber} from your {withdrawlAccountType} account.");

            break;

          case "transfer":
            Console.Clear();
            Console.WriteLine("What account do you want to transfer from (CHECKING) or (SAVINGS)");
            var transferAccountType = Console.ReadLine().ToLower();
            transferAccountType = verifyAccount(transferAccountType);

            Console.WriteLine("How much will you be transfering?");
            var transferAmount = Console.ReadLine().ToLower();
            decimal transferNumber = verifyAmount(transferAmount);

            bankContext.TransferMoney(transferAccountType, transferNumber, userName);

            Console.Clear();
            if (transferAccountType == "checking")
            {
              Console.WriteLine($"I transferred {transferAmount} from your Checking account to your Savings account.");
            }
            else
            {
              Console.WriteLine($"I transferred {transferAmount} from your Savings account to your Checking account.");
            }

            break;

          case "balance":
            Console.Clear();
            bankContext.SeeTotals(userName);
            break;

          case "quit":
            isRunning = false;
            break;
        }
      }
      Console.WriteLine("Thank you for banking with SDG Bank.");
    }
  }
}









