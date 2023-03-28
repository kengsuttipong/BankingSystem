using BankingSystem_Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingSystem_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BankingSystemChallengeContext _context;

        public DataController(IHttpContextAccessor httpContextAccessor,
                             BankingSystemChallengeContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        [HttpGet("CheckPasscode")]
        public IActionResult CheckPasscode(string passcode)
        {
            var userID = "";
            if (_httpContextAccessor != null)
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var myAccount = _context.Accounts.Where(i => i.CustomerId == userID).FirstOrDefault();

            if (myAccount.Passcode == passcode)
            {
                return Ok(new { isSuccess = true });
            }
            else
            {
                return Ok(new { isSuccess = false });
            }
        }
    }
}
