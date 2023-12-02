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
  public string Username { get; set; }

  [Required]
  public string Email { get; set; }

  [StringLength(96, MinimumLength = 1, ErrorMessage = "Names must be between 1 and 96 characters")]
  public string Name { get; set; }

  [StringLength(10000, MinimumLength = 1, ErrorMessage = "About sections have a max length of 10000 characters")]
  public string About { get; set; }

  [Required]
  public string AccountType { get; set; } = "User";

  [Required]
  public DateTime CreationDateUTC { get; init; }

  [Required]
  public bool Disabled { get; set; } = false;
}