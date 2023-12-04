using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPortal.Models;
using AdminPortal.ViewModels;

namespace AdminPortal.Controllers;

public class ArticleController : Controller
{
    private readonly AdminPortalDbContext _context;

    public ArticleController(AdminPortalDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Articles.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["PublisherId"] = new SelectList(_context.Accounts, "AccountId", "AccountType");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleVM articleVM)
    {
        string authorEmail = HttpContext.Session.GetString(nameof(Account.Email));
        Console.WriteLine("\n\n!!!EMAIL IS " + authorEmail);
        Account author = _context.Accounts.FirstOrDefault(x => x.Email == authorEmail);
        Console.WriteLine("\n\n!!!AUTHOR IS " + author);
        int authorId = author.AccountId;

        bool hidden = false;
        if(articleVM.Hidden == true){ hidden = true; }
        
        if (ModelState.IsValid)
        {
            Article article = new Article{
                AuthorId = authorId,
                ArticleTitle = articleVM.ArticleTitle,
                ArticleSummary = articleVM.ArticleSummary,
                ArticleBody = articleVM.ArticleBody,
                CreationTimeUTC = DateTime.Now,
                PublishTimeUTC = null,
                Status = "Draft",
                Hidden = hidden
            };
            _context.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(articleVM);
    }    
}
