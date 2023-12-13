using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminPortal.Models;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
  private readonly AdminPortalDbContext _context;

  public ArticleController(AdminPortalDbContext context)
  {
      _context = context;
  }

  // GET: api/Article
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Article>>> GetArticleList()
  {
    if (_context.Articles == null)
    {
        return NotFound();
    }
    return await _context.Articles.OrderByDescending(x => x.CreationTimeUTC).ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<IEnumerable<Article>>> GetArticle(int id)
  {
    if (_context.Articles == null)
    {
        return NotFound();
    }
    return await _context.Articles.Where(x => x.ArticleId == id).Include(x => x.Author).ToListAsync();
  }
}