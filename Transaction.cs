using System;

namespace SdgBank
{
  public class Transaction
  {
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    public string TransactionType { get; set; }

    public string AccountType { get; set; }

    public decimal TransactionAmount { get; set; }
    public string UserName { get; set; }

  }

}