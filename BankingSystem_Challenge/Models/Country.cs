using System;
using System.Collections.Generic;

namespace BankingSystem_Challenge.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? CountryCode { get; set; }
}
