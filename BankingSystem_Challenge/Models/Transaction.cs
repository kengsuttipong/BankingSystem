using System;
using System.Collections.Generic;

namespace BankingSystem_Challenge.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? FromAccountId { get; set; }

    public string? ToIban { get; set; }

    public double? Amount { get; set; }

    public string? Currency { get; set; }

    public double? TransactionFee { get; set; }

    public DateTime? DateExecuted { get; set; }
}
