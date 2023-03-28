namespace BankingSystem_Challenge.ViewModels
{
    public class TransactionListViewModel
    {
        public string? TransactionType { get; set; }
        public string? AccountName { get; set; }
        public string? Iban { get; set; }
        public double? Amount { get; set; }
        public DateTime? DateExcute { get; set; }
    }
}
