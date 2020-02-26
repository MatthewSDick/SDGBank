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
    public List<User> userList = new List<User>();
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

    // public void loadAccounts()
    // {
    //   var reader = new StreamReader("accounts.csv");
    //   var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
    //   accountsList = csvReader.GetRecords<Account>().ToList();
    // }

    public void loadTransactions()
    {
      var reader = new StreamReader("transactions.csv");
      var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      transactionsList = csvReader.GetRecords<Transaction>().ToList();
    }

    public void loadUsers()
    {
      var reader = new StreamReader("users.csv");
      var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
      userList = csvReader.GetRecords<User>().ToList();
    }

    // public void SeeTotals(string accountName)
    // {
    //   Console.Clear();
    //   Console.WriteLine($"Your current account balances are...");
    //   accountsList = accountsList.Where(x => x.UserName.ToLower() == accountName).ToList();
    //   for (int i = 0; i < accountsList.Count; i++)
    //   {
    //     Console.WriteLine($" - {accountsList[i].AccountType} = {accountsList[i].Balance}");
    //   }
    //loadAccounts();
  }

  // public void MakeDeposit(string accountType, decimal amount, string user)
  // {
  //   accountsList.Find(account => account.AccountType == accountType && account.UserName.ToLower() == user).Balance += amount;
  //   SaveAccounts();
  //   transactionsList.Add(new Transaction { UserName = user, TransactionType = "deposit", AccountType = accountType, TransactionAmount = amount });
  //   SaveTransactions();
  // }

  // public void MakeWithdrawl(string accountType, decimal amount, string user)
  // {
  //   accountsList.First(account => account.AccountType == accountType && account.UserName.ToLower() == user).Balance -= amount;
  //   SaveAccounts();
  //   transactionsList.Add(new Transaction { UserName = user, TransactionType = "deposit", AccountType = accountType, TransactionAmount = amount });
  //   SaveTransactions();
  // }

  // public void TransferMoney(string fromAccount, decimal amount, string user)
  // {
  //   var transferType = "";
  //   if (fromAccount == "checking")
  //   {
  //     var toAccount = "savings";
  //     accountsList.Find(account => account.AccountType == fromAccount && account.UserName.ToLower() == user).Balance -= amount;
  //     accountsList.Find(account => account.AccountType == toAccount && account.UserName.ToLower() == user).Balance += amount;
  //     transferType = "C->S";
  //   }
  //   else
  //   {
  //     var toAccount = "checking";
  //     accountsList.Find(account => account.AccountType == fromAccount && account.UserName.ToLower() == user).Balance -= amount;
  //     accountsList.Find(account => account.AccountType == toAccount && account.UserName.ToLower() == user).Balance += amount;
  //     transferType = "S->C";
  //   }
  //   SaveAccounts();
  //   transactionsList.Add(new Transaction { UserName = user, TransactionType = "transfer", AccountType = transferType, TransactionAmount = amount });
  //   SaveTransactions();
  // }


}
