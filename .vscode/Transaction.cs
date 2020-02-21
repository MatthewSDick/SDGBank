using System;

namespace SdgBank
{

  public class Transaction
  {
    public string TransactionType { get; set; }

    public string AccountType { get; set; }

    public string AccountOwner { get; set; }

    public decimal TransactionAmount { get; set; }

    public DateTime TranscationTime { get; set; }

  }
}