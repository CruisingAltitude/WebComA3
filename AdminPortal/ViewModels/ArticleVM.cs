using System.ComponentModel.DataAnnotations;

namespace AdminPortal.ViewModels;

public class ArticleVM
{
  public int? ArticleId { get; init; }
  [Required]
  public string ArticleTitle { get; set; }

  [Required]
  public string ArticleSummary { get; set; }

  [Required]
  public string ArticleBody { get; set; }

  public string Status { get; set; }

  public bool Hidden { get; set; }
}