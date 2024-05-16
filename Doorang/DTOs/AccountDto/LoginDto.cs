using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace Doorang.DTOs.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered {  get; set; }
    }
}
