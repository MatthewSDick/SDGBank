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
        var input = "";
        while (toCheck != "checking" && toCheck != "savings")
        {
          Console.Clear();
          Console.WriteLine("Please choose either (CHECKING) or (SAVINGS)");
          input = Console.ReadLine().ToLower();
        }
        return input;
      }

      // The app should save my transactions to file using a standard format.This saving should happen after every transaction
      // The app should load up past transaction from a file on start up.
      Console.Clear();
      Console.WriteLine("$$$  Welcome to SDG Bank.  $$$");
      Console.WriteLine("");

      // open and read the Balance file
      // Logic here to display balances
      // As a user I should be able see the totals in my saving and checking account when the program first starts
      var bankManager = new BankManager();
      bankManager.loadAccounts();
      bankManager.loadTransactions();

      var accounts = new List<Account>();

      //bankManager.SeeTotals();


      var isRunning = true;
      while (isRunning)
      {
        bankManager.SeeTotals();
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

            decimal depositNumber;
            while (!decimal.TryParse(depositAmount, out depositNumber))
            {
              Console.WriteLine("Please enter a valid money amount.  Example 13.45.");
              depositAmount = Console.ReadLine().ToLower();
            }

            // Call the function in BankManager
            Console.Clear();
            bankManager.MakeDeposit(depositAccountType, depositNumber);
            Console.WriteLine($"I have deposited {depositNumber} into your {depositAccountType} account.");

            break;

          case "withdrawl":

            Console.Clear();
            Console.WriteLine("What account do you want to withdrawl from (CHECKING) or (SAVINGS)");
            var withdrawlAccountType = Console.ReadLine().ToLower();

            depositAccountType = verifyAccount(withdrawlAccountType);

            Console.WriteLine("How much will you be withdrawling?");
            var withdrawlAmount = Console.ReadLine().ToLower();

            decimal withdrawlNumber;
            while (!decimal.TryParse(withdrawlAmount, out withdrawlNumber))
            {
              Console.WriteLine("Please enter a valid money amount.  Example 13.45.");
              withdrawlAmount = Console.ReadLine().ToLower();
            }
            Console.Clear();
            bankManager.MakeWithdrawl(withdrawlAccountType, withdrawlNumber);
            Console.WriteLine($"You withdrew {withdrawlNumber} from your {withdrawlAccountType} account.");

            break;

          case "transfer":
            Console.Clear();
            Console.WriteLine("What account do you want to transfer from (CHECKING) or (SAVINGS)");
            var transferAccountType = Console.ReadLine().ToLower();

            depositAccountType = verifyAccount(transferAccountType);

            Console.WriteLine("How much will you be transfering?");
            var transferAmount = Console.ReadLine().ToLower();

            decimal transferNumber;
            while (!decimal.TryParse(transferAmount, out transferNumber))
            {
              Console.WriteLine("Please enter a valid money amount.  Example 13.45.");
              transferAmount = Console.ReadLine().ToLower();
            }

            bankManager.TransferMoney(transferAccountType, transferNumber);
            // Call the function in BankManager

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
            bankManager.SeeTotals();

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









