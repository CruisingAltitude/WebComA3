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
    public async Task<ActionResult<IEnumerable<Article>>> GetArticle()
    {
      if (_context.Articles == null)
      {
          return NotFound();
      }
      return await _context.Articles.ToListAsync();
    }
}