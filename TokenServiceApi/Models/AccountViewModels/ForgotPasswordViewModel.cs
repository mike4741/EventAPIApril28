using System.ComponentModel.DataAnnotations;

namespace TokenServiceApi.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
