using System.ComponentModel.DataAnnotations;

namespace AdminPortal.ViewModels;

public class AccountVM{
  [Required]
  [RegularExpression(@"^[\w]{5,32}$")]
  [StringLength(32, MinimumLength = 5, ErrorMessage = "Usernames must be between 4 and 33 characters")]
  public string Username { get; set; } = string.Empty;

  [Required]
  public string Email { get; set; } = string.Empty;

  [StringLength(96, MinimumLength = 1, ErrorMessage = "Names must be between 1 and 96 characters")]
  public string Name { get; set; } = string.Empty;

  [StringLength(10000, MinimumLength = 1, ErrorMessage = "About sections have a max length of 10000 characters")]
  public string About { get; set; } = string.Empty;
}