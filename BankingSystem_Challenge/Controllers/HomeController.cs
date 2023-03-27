using BankingSystem_Challenge.Models;
using BankingSystem_Challenge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

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
            return View();
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