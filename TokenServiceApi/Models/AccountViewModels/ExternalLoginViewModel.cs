using System.ComponentModel.DataAnnotations;

namespace TokenServiceApi.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
