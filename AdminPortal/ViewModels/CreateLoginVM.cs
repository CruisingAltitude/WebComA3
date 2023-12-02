using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.ViewModels;

public class CreateLoginVM{
  [Required]
  public string Email { get; set; }

  [Required]
  [Column(TypeName = "char")]
  [StringLength(96, MinimumLength = 6, ErrorMessage = "Passwords must be between 6 and 96 characters")]
  public string Password { get; set; }

  [Required]
  public string PasswordConfirm { get; set; }
}