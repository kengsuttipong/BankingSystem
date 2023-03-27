using BankingSystem_Challenge.Models;
using BankingSystem_Challenge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankingSystem_Challenge.Controllers
{
    [Authorize]
    public class FinancialTransactionsController : Controller
    {
        private readonly BankingSystemChallengeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FinancialTransactionsController(BankingSystemChallengeContext context
                                                ,UserManager<ApplicationUser> userManager
                                                ,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Verify()
        {
            var userID = "";
            if (_httpContextAccessor != null)
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var account = _context.Accounts.Where(i => i.CustomerId == userID).FirstOrDefault();

            var verifyAccount = new VerifyAccount()
            {
                FromIban = account.Iban,
                Balance = account.Balance,
            };

            return View(verifyAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(VerifyAccount model)
        {
            if (ModelState.IsValid)
            {
                model.Fee = model.Amount * (0.001);
                model.Amount = model.Amount - model.Fee;
                return RedirectToAction("Transfer", model);
            }

            var userID = "";
            if (_httpContextAccessor != null)
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var account = _context.Accounts.Where(i => i.CustomerId == userID).FirstOrDefault();

            model.FromIban = account.Iban;
            model.Balance = account.Balance;

            return View(model);
        }

        public IActionResult Transfer(VerifyAccount model)
        {
            if (ModelState.IsValid) 
            {
                var accountFrom = from account in _context.Accounts
                            join user in _context.Users on account.CustomerId equals user.Id
                            where account.Iban == model.FromIban
                            select new { account.AccountId, user.first_name, user.last_name, account.Iban };

                var accountTo = from account in _context.Accounts
                                  join user in _context.Users on account.CustomerId equals user.Id
                                  where account.Iban == model.ToIBAN
                                  select new { account.AccountId, user.first_name, user.last_name, account.Iban };

                var from = accountFrom.FirstOrDefault();
                var to = accountTo.FirstOrDefault();

                model.FromName = from.first_name + " " + from.last_name.ToString();
                model.ToName = to.first_name.ToString() + " " + to.last_name.ToString();

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferMoney(VerifyAccount model)
        {
            if (ModelState.IsValid)
            {
                var accountFrom = await _context.Accounts.Where(i => i.Iban == model.FromIban).FirstOrDefaultAsync();

                if (accountFrom != null) 
                {
                    accountFrom.Balance = accountFrom.Balance - (model.Amount + model.Fee);
                }

                var accountTo = await _context.Accounts.Where(i => i.Iban == model.ToIBAN).FirstOrDefaultAsync();

                if (accountTo != null)
                {
                    accountTo.Balance = accountTo.Balance + model.Amount;
                }

                Transaction transaction = new Transaction()
                {
                    FromAccountId = accountFrom.AccountId,
                    ToIban = accountTo.Iban,
                    Amount = model.Amount,
                    Currency = "THB",
                    TransactionFee = model.Fee,
                    DateExecuted = DateTime.Now,
                };

                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync();

                model.DateExcute = DateTime.Now;

                return RedirectToAction("Slip", model);
            }

            return View(model);
        }

        public IActionResult Slip(VerifyAccount model)
        { 
            return View(model);
        }
    }
}
