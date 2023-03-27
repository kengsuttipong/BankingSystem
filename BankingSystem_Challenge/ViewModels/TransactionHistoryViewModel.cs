namespace BankingSystem_Challenge.ViewModels
{
    public class TransactionHistoryViewModel
    {
        public double? balance { get; set; }
        public string? iban { get; set; }
        public List<TransactionListViewModel> transactionListViewModels { get; set; }
    }
}
