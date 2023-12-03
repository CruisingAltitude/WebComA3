using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.ViewModels;

public class LoginVM{
  [Required]
  public string Email { get; set; }

  [Required]
  [Column(TypeName = "char")]
  [StringLength(94)]
  public string Password { get; set; }
}