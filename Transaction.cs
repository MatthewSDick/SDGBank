using System;

namespace SdgBank
{
  public class Transaction
  {
    public int Account_ID { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
    public string TransactionType { get; set; }
    public string AccountType { get; set; }
    public decimal TransactionAmount { get; set; }
    public int ID { get; set; }
    public string UserName { get; set; }

  }

}