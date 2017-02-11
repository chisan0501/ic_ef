using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace ic_ef.Models
{
    public class IndexViewModel
    {
        public string refrub_name { get; set; }
        public bool isAdmin { get; set; }
        public string Department { get; set; }
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class inventoryManageViewModel
    {
        [Required(ErrorMessage = "Asset Tag required!", AllowEmptyStrings = false)]
        public int ictags { get; set; }
        [Required(ErrorMessage = "Pallet Name required!", AllowEmptyStrings = false)]
        public string pallet { get; set; }
        public int SelectedPalletId { get; set; }
        public SelectList PalletList { get; set; }
        

    }
  
    public class graphViewModel
    {
        public string time { get; set; }
        public int desktop_six_am { get; set; }
        public int desktop_seven_am { get; set; }
        public int desktop_eight_am { get; set; }
        public int desktop_nine_am { get; set; }
        public int desktop_ten_am { get; set; }
        public int desktop_eleven_am { get; set; }
        public int desktop_twelve_pm { get; set; }
        public int desktop_one_pm { get; set; }
        public int desktop_two_pm { get; set; }
        public int desktop_three_pm { get; set; }
        public int desktop_four_pm { get; set; }
        public int desktop_five_pm { get; set; }
        public int desktop_six_pm { get; set; }
        public int laptop_six_am { get; set; }
        public int laptop_seven_am { get; set; }
        public int laptop_eight_am { get; set; }
        public int laptop_nine_am { get; set; }
        public int laptop_ten_am { get; set; }
        public int laptop_eleven_am { get; set; }
        public int laptop_twelve_pm { get; set; }
        public int laptop_one_pm { get; set; }
        public int laptop_two_pm { get; set; }
        public int laptop_three_pm { get; set; }
        public int laptop_four_pm { get; set; }
        public int laptop_five_pm { get; set; }
        public int laptop_six_pm { get; set; }
        public int day1 { get; set; }
        public int day2 { get; set; }
        public int day3 { get; set; }
        public int day4 { get; set; }
        public int day5 { get; set; }
        public int day6 { get; set; }
        public int day7 { get; set; }
        public int day8 { get; set; }
        public int dis_day1 { get; set; }
        public int dis_day2 { get; set; }
        public int dis_day3 { get; set; }
        public int dis_day4 { get; set; }
        public int dis_day5 { get; set; }
        public int dis_day6 { get; set; }
        public int dis_day7 { get; set; }
        public int dis_day8 { get; set; }
    }

   

    public class reportViewModel
    {
        public string count_pallet { get; set; }

    }
    public class adminViewModel
    {

    }
    public class ChangeDepartmentViewModel
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "New Department")]
        public string NewDepartment { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}