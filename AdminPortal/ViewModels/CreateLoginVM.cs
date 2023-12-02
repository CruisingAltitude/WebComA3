using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.ViewModels;

public class CreateLoginVM{
  [Required]
  public string Email { get; set; }

  [Required]
  [RegularExpression(@"^[\w]{5,32}$")]
  [StringLength(32, MinimumLength = 5, ErrorMessage = "Usernames must be between 4 and 33 characters")]
  public string Username { get; set; }

  [Required]
  [Column(TypeName = "char")]
  [StringLength(96, MinimumLength = 6, ErrorMessage = "Passwords must be between 6 and 96 characters")]
  public string Password { get; set; }

  [Required]
  public string PasswordConfirm { get; set; }
}