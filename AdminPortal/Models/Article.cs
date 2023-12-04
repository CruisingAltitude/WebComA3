using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.Models;

public class Article{
  [Key]
  [Required]
  public int ArticleId { get; init; }

  [ForeignKey("Account")]
  public int AuthorId { get; init; }
  public virtual Account Author { get; init; }

  [Required]
  public string ArticleTitle { get; set; }

  public string ArticleSummary { get; set; }

  public string ArticleBody { get; set; }

  [Required]
  [DataType(DataType.Date)]
  public DateTime CreationTimeUTC { get; init; }

  [DataType(DataType.Date)]
  public DateTime? PublishTimeUTC { get; init; }

  public string Status { get; set; }

  [Required]
  [DefaultValue("false")]
  public bool Hidden { get; set; }
}