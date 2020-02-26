using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SdgBank;
using System.Linq;


namespace SdgBank
{
  public partial class BankContext : DbContext
  {

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public void SeeAccounts()
    {
      var bankUsers = Users.ToList();
      for (int i = 0; i < bankUsers.Count; i++)
      {
        Console.WriteLine($"{bankUsers[i].UserName}");
      }

      // for (int i = 0; i < Users.Count(); i++)
      // {
      //   Console.WriteLine($"{Users[i].}");
      // }
    }

    public bool ValidateUser(string username)
    {

      if (Users.Count(user => user.UserName.ToLower() == username) > 0)
      {
        return true;
      }
      else
      {
        return false;
      }

    }

    public bool ValidatePassword(string userPassword)
    {

      if (Users.Count(user => user.Password.ToLower() == userPassword) > 0)
      {
        return true;
      }
      else
      {
        return false;
      }

    }

    public void MakeDeposit(string accountType, decimal amount, string user)
    {

      Accounts.First(account => account.AccountType.ToLower() == accountType && account.UserName.ToLower() == user).Balance += amount;
      Transactions.Add(new Transaction { UserName = user, TransactionType = "deposit", AccountType = accountType, TransactionAmount = amount });
      SaveChanges();
      //SaveTransactions();

    }

    public void MakeWithdrawl(string accountType, decimal amount, string user)
    {
      Accounts.First(account => account.AccountType.ToLower() == accountType && account.UserName.ToLower() == user).Balance -= amount;
      Transactions.Add(new Transaction { UserName = user, TransactionType = "deposit", AccountType = accountType, TransactionAmount = amount });
      SaveChanges();
    }

    public void TransferMoney(string fromAccount, decimal amount, string user)
    {
      var transferType = "";
      if (fromAccount == "checking")
      {
        var toAccount = "savings";
        Accounts.First(account => account.AccountType.ToLower() == fromAccount && account.UserName.ToLower() == user).Balance -= amount;
        Accounts.First(account => account.AccountType.ToLower() == toAccount && account.UserName.ToLower() == user).Balance += amount;
        transferType = "C->S";
      }
      else
      {
        var toAccount = "checking";
        Accounts.First(account => account.AccountType.ToLower() == fromAccount && account.UserName.ToLower() == user).Balance -= amount;
        Accounts.First(account => account.AccountType.ToLower() == toAccount && account.UserName.ToLower() == user).Balance += amount;
        transferType = "S->C";
      }
      Transactions.Add(new Transaction { UserName = user, TransactionType = "transfer", AccountType = transferType, TransactionAmount = amount });
      SaveChanges();
    }

    public void SeeTotals(string accountName)
    {
      Console.Clear();
      Console.WriteLine($"Your current account balances are...");
      var accountsList = Accounts.Where(x => x.UserName.ToLower() == accountName).ToList();
      for (int i = 0; i < accountsList.Count; i++)
      {
        Console.WriteLine($" - {accountsList[i].AccountType} = {accountsList[i].Balance}");
      }
      //loadAccounts();
    }







    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        optionsBuilder.UseNpgsql("server=localhost;database=SDGBank");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
