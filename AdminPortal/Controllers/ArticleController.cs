using Microsoft.EntityFrameworkCore;
using AdminPortal.Models;
using AdminPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdminPortal.Controllers;

public class ArticleController{
  private readonly DbContext _context;
  
  public ArticleController(DbContext context){
    _context = context;
  }

  public async Task<IActionResult> Index()
  {
      var portfolioDbContext = await _context.Articles.Include(a => a.Account);
      return View(portfolioDbContext.ToListAsync());
  }
}