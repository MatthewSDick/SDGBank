using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace SdgBank
{


  public class BankManager
  {

    public List<Account> accountsList = new List<Account>();
    public List<Transaction> transactionsList = new List<Transaction>();
    public void SaveAccounts()
    {
      var writer = new StreamWriter("accounts.csv");
      var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
      csvWriter.WriteRecords(accountsList);
      writer.Flush();
    }

    public void SaveTransactions()
    {
      var writer = new StreamWriter("transactions.csv");
      var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
      csvWriter.WriteRecords(transactionsList);
      writer.Flush();
    }

    public void loadAccounts()
    {
      var reader = new StreamReader("accounts.csv");
      var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      accountsList = csvReader.GetRecords<Account>().ToList();
    }

    public void loadTransactions()
    {
      var reader = new StreamReader("transactions.csv");
      var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      transactionsList = csvReader.GetRecords<Transaction>().ToList();
    }

    // As a user I should be able see the totals in my saving and checking account when the program first starts
    public void SeeTotals()
    {
      Console.WriteLine($"Your current account balances are...");
      for (int i = 0; i < accountsList.Count; i++)
      {
        Console.WriteLine($" - {accountsList[i].AccountType} = {accountsList[i].Balance}");
      }
    }
    // As a user I should be able to deposit funds to my savings account
    // As a user I should be able to deposit funds to my checking account
    public void MakeDeposit(string accountType, decimal amount)
    {
      accountsList.Find(account => account.AccountType == accountType).Balance += amount;
      SaveAccounts();
      transactionsList.Add(new Transaction { TransactionType = "deposit", AccountType = accountType, TransactionAmount = amount });
      SaveTransactions();
    }

    // As a user I should be able to withdraw funds to my savings account
    // As a user I should be able to withdraw funds to my checking account
    public void MakeWithdrawl(string accountType, decimal amount)
    {
      accountsList.Find(account => account.AccountType == accountType).Balance -= amount;
      SaveAccounts();
      transactionsList.Add(new Transaction { TransactionType = "withdrawal", AccountType = accountType, TransactionAmount = amount });
      SaveTransactions();
    }

    // As a user I should be able to transfer funds from my checking account to my savings account
    // As a user I should be able to transfer funds from my savings account to my checking accounts
    public void TransferMoney(string fromAccount, decimal amount)
    {
      var transferType = "";
      if (fromAccount == "checking")
      {
        var toAccount = "savings";
        accountsList.Find(account => account.AccountType == fromAccount).Balance -= amount;
        accountsList.Find(account => account.AccountType == toAccount).Balance += amount;
        transferType = "C->S";
      }
      else
      {
        var toAccount = "checking";
        accountsList.Find(account => account.AccountType == fromAccount).Balance -= amount;
        accountsList.Find(account => account.AccountType == toAccount).Balance += amount;
        transferType = "S->C";
      }
      SaveAccounts();
      transactionsList.Add(new Transaction { TransactionType = "transfer", AccountType = transferType, TransactionAmount = amount });
      SaveTransactions();
    }


    // The app should save my transactions to file using a standard format. This saving should happen after every transaction
    // The app should load up past transaction from a file on start up.


  }
}