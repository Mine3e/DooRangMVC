using System.ComponentModel.DataAnnotations;

namespace Doorang.DTOs.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name {  get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Surname {  get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Usurname { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword {  get; set; }
    }
}
