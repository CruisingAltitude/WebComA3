using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.Models;

public class ArticleUpdate{
  [Key]
  [Required]
  public int UpdateId { get; init; }

  [Required]
  [ForeignKey("Article")]
  public int ArticleId { get; init; }
  public virtual Article Article { get; init; }

  [Required]
  [ForeignKey("Account")]
  public int UpdaterId { get; init; }
  public virtual Account Account { get; init; }

  [Required]
  [DataType(DataType.Date)]
  public DateTime UpdateTimeUTC { get; init; }

  [Required]
  public string PreviousField { get; init; } = string.Empty;

  [Required]
  public string PreviousValue { get; init; } = string.Empty;
}