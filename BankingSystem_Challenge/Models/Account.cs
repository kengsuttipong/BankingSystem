using System;
using System.Collections.Generic;

namespace BankingSystem_Challenge.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? CustomerId { get; set; }

    public string? Iban { get; set; }

    public string? AccountType { get; set; }

    public string? Currency { get; set; }

    public double? Balance { get; set; }

    public DateTime? DateCreated { get; set; }
    public string Passcode { get; set; }
}
