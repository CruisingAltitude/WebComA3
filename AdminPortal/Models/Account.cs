using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminPortal.Models;

public class Account{
  [Key]
  [Required]
  public int AccountId { get; init; }

  [Required]
  [RegularExpression(@"^[\w]{5,32}$")]
  [StringLength(32, MinimumLength = 5, ErrorMessage = "Usernames must be between 4 and 33 characters")]
  public string Username { get; set; } = string.Empty;

  [Required]
  
  public string Email { get; set; } = string.Empty;

  [Required]
  [DefaultValue("User")]
  public string AccountType { get; set; } = string.Empty;

  [Required]
  public DateTime CreationDateUTC { get; init; }

  [Required]
  public bool Disabled { get; set; } = false;
}