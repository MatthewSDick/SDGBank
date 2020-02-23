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
      var validUsers = new List<User>();

      Console.Clear();
      Console.WriteLine("$$$  Welcome to SDG Bank.  $$$");
      Console.WriteLine("");

      var bankManager = new BankManager();
      bankManager.loadAccounts();
      bankManager.loadTransactions();
      bankManager.loadUsers();

      //var accounts = new List<Account>();

      var isRunning = true;
      while (isRunning)
      {
        // Display a list of users
        Console.WriteLine("Please choose an user.");
        // Call display accounts
        validUsers = bankManager.SeeAccounts();
        for (int i = 0; i < validUsers.Count; i++)
        {
          Console.WriteLine($"({validUsers[i].UserName}) Account:{validUsers[i].AccountNumber}");
        }

        var has = false;
        var selectedUser = "";
        while (has == false)
        {
          Console.WriteLine("Please select an account name.");
          selectedUser = Console.ReadLine().ToLower();
          has = validUsers.Any(cus => cus.UserName.ToLower() == selectedUser);
        }

        bankManager.SeeTotals(selectedUser);
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
            bankManager.MakeDeposit(depositAccountType, depositNumber, selectedUser);
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
            bankManager.MakeWithdrawl(withdrawlAccountType, withdrawlNumber, selectedUser);
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

            bankManager.TransferMoney(transferAccountType, transferNumber, selectedUser);

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
            //bankManager.SeeTotals();

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









