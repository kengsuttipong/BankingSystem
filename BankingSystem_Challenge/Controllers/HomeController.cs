using BankingSystem_Challenge.Models;
using BankingSystem_Challenge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingSystem_Challenge.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BankingSystemChallengeContext _context;

        public HomeController(ILogger<HomeController> logger
                            , UserManager<ApplicationUser> userManager,
                              IHttpContextAccessor httpContextAccessor,
                              BankingSystemChallengeContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userID = "";
            if (_httpContextAccessor != null)
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var user = _context.Users.Where(i => i.Id == userID).FirstOrDefault();
            var account = _context.Accounts.Where(i => i.CustomerId == userID).FirstOrDefault();

            var home = new HomeViewModel()
            {
                first_name = user.first_name,
                last_name = user.last_name,
                email = user.Email,
                phonenumber = user.PhoneNumber,
                balance = account.Balance,
                iban = account.Iban,
            };

            return View(home);
        }

        public IActionResult TransactionHistory()
        {
            var userID = "";
            if (_httpContextAccessor != null)
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var myAccount = _context.Accounts.Where(i => i.CustomerId == userID).FirstOrDefault();

            var myTransaction = from t in _context.Transactions
                                join a1 in _context.Accounts on t.FromAccountId equals a1.AccountId
                                join a2 in _context.Accounts on t.ToIban equals a2.Iban
                                join u in _context.Users on a1.CustomerId equals u.Id
                                join u2 in _context.Users on a2.CustomerId equals u2.Id
                                where u.Id == userID || u2.Id == userID
                              select new TransactionListViewModel
                              {
                                  TransactionType = t.FromAccountId == myAccount.AccountId ? "Transfer To" : "Received From",
                                  AccountName = t.FromAccountId == myAccount.AccountId ? u2.first_name + ' ' + u2.last_name : u.first_name + ' ' + u.last_name,
                                  Iban = t.FromAccountId == myAccount.AccountId ? a2.Iban : a1.Iban,
                                  Amount = t.Amount,
                                  DateExcute = t.DateExecuted
                              };

            var myTransactionList = myTransaction.ToList();

            TransactionHistoryViewModel transactionHistoryViewModel = new TransactionHistoryViewModel()
            {
                balance = myAccount.Balance,
                iban = myAccount.Iban,
                transactionListViewModels = myTransactionList,
            };

            return View(transactionHistoryViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}