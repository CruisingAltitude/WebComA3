using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.Models;

public class Login{
  [Key]
  [Required]
  public int LoginId { get; init; }

  [Required]
  [ForeignKey("Account")]
  public int AccountId { get; init; }
  public virtual Account Account { get; init; }

  [Required]
  [Column(TypeName = "char")]
  [StringLength(96, MinimumLength = 6, ErrorMessage = "Passwords must be between 6 and 96 characters")]
  public string PasswordHash { get; init; }
}