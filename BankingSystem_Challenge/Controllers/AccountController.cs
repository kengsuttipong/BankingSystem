using BankingSystem_Challenge.Models;
using BankingSystem_Challenge.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingSystem_Challenge.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankingSystemChallengeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(BankingSystemChallengeContext context,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Fail, Username or Password is invalid.");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            ViewData["CountriesLists"] = new SelectList(_context.Countries, "CountryCode", "CountryName");
            ViewData["AccountTypeLists"] = new SelectList(_context.AccountTypes, "AccountTypeId", "AccountTypeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    first_name = model.FirstName,
                    last_name = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth,
                    UserName = model.Username,
                    address = model.Address,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string userID = user.Id;

                    var account = new Account()
                    {
                        CustomerId = userID,
                        Iban = model.IBAN,
                        AccountType = model.AccountType,
                        Balance = model.Balance,
                        DateCreated = DateTime.Now,
                        Passcode = model.Passcode,
                    };

                    _context.Add(account);
                    _context.SaveChanges();

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            ViewData["CountriesLists"] = new SelectList(_context.Countries, "CountryCode", "CountryName");
            ViewData["AccountTypeLists"] = new SelectList(_context.AccountTypes, "AccountTypeId", "AccountTypeName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
