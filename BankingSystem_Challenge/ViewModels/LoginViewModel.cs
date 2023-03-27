using System.ComponentModel.DataAnnotations;

namespace BankingSystem_Challenge.ViewModels
{
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "Username can not be blank")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Password can not be blank")]
        public string Password { get; set; }
    }
}
