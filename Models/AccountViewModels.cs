using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        //[Display(Name = "مرورگر را به خاطر بسپار؟")]
        //public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "كلمه عبور")]
        //[EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "كلمه عبور")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز عبور")]
        [Compare("رمز عبور", ErrorMessage = "رمز عبور و تایید آت مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "كلمه عبور")]
        public string UserName { get; set; }
    }

    public static class UserTest
    {
        public static bool IsValidUser = false;
        public static string ValidUser="";
        public static string LastName = "";
        public static string Name = "";
        public static bool UserType = false;
    }
}
