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

        if(authorEmail == null)
        { 
            ModelState.AddModelError("EmailNull", "You are not logged in.");
            return View(articleVM);
        }

        Account author = _context.Accounts.FirstOrDefault(x => x.Email == authorEmail);
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

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Articles == null)
        {
            return NotFound();
        }

        var article = await _context.Articles.Include(x => x.Author).FirstOrDefaultAsync(m => m.ArticleId == id);
        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id == null || _context.Articles == null)
        {
            return NotFound();
        }

        var article = await _context.Articles.FindAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        ViewData["PublisherId"] = new SelectList(_context.Accounts, "AccountId", "AccountType", article.AuthorId);
        return View(article);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ArticleVM articleVM)
    {
        if (id != articleVM.ArticleId)
        {
            return NotFound();
        }
        string authorEmail = HttpContext.Session.GetString(nameof(Account.Email));

        if(authorEmail == null)
        { 
            ModelState.AddModelError("EmailNull", "You are not logged in.");
            return View(articleVM);
        }


        Account author = _context.Accounts.FirstOrDefault(x => x.Email == authorEmail);
        int authorId = author.AccountId;
        int updaterId = (int) HttpContext.Session.GetInt32(nameof(Account.AccountId));

        if (ModelState.IsValid)
        {

            // Get article by id
            Article article = _context.Articles.FirstOrDefault(x => x.ArticleId == articleVM.ArticleId);
            Account updater = _context.Accounts.FirstOrDefault(x => x.AccountId == updaterId);

            // Update article title
            if(article.ArticleTitle != articleVM.ArticleTitle){
                ArticleUpdate updateTitle = new ArticleUpdate{
                    ArticleId = (int)articleVM.ArticleId,
                    UpdaterId = updater.AccountId,
                    UpdateTimeUTC = DateTime.Now,
                    PreviousField = nameof(article.ArticleTitle),
                    PreviousValue = article.ArticleTitle
                };
                article.ArticleTitle = articleVM.ArticleTitle;
                _context.ArticleUpdates.Add(updateTitle);
            }

           // Update article summary
            if(article.ArticleTitle != articleVM.ArticleTitle){
                ArticleUpdate updateSummary = new ArticleUpdate{
                    ArticleId = (int)articleVM.ArticleId,
                    UpdaterId = updater.AccountId,
                    UpdateTimeUTC = DateTime.Now,
                    PreviousField = nameof(article.ArticleSummary),
                    PreviousValue = article.ArticleSummary
                };
                article.ArticleSummary = articleVM.ArticleSummary;
                _context.ArticleUpdates.Add(updateSummary);
            }

            // Update article body
            if(article.ArticleBody != articleVM.ArticleBody){
                ArticleUpdate updateBody = new ArticleUpdate{
                    ArticleId = (int)articleVM.ArticleId,
                    UpdaterId = updaterId,
                    UpdateTimeUTC = DateTime.Now,
                    PreviousField = nameof(article.ArticleBody),
                    PreviousValue = article.ArticleBody
                };
                article.ArticleBody = articleVM.ArticleBody;
                _context.ArticleUpdates.Add(updateBody);
            }

            // Update article status
            if(article.Status != articleVM.Status){
                ArticleUpdate updateStatus = new ArticleUpdate{
                    ArticleId = (int)articleVM.ArticleId,
                    UpdaterId = updaterId,
                    UpdateTimeUTC = DateTime.Now,
                    PreviousField = nameof(article.Status),
                    PreviousValue = article.Status
                };
                article.Status = articleVM.Status;
                _context.ArticleUpdates.Add(updateStatus);
            }

            // Update article hidden
            if(article.Hidden != articleVM.Hidden){
                ArticleUpdate updateHidden = new ArticleUpdate{
                    ArticleId = (int)articleVM.ArticleId,
                    UpdaterId = updaterId,
                    UpdateTimeUTC = DateTime.Now,
                    PreviousField = nameof(article.Hidden),
                    PreviousValue = article.Hidden.ToString()
                };
                article.Hidden = articleVM.Hidden;
                _context.ArticleUpdates.Add(updateHidden);
            }

            try
            {
                _context.Update(article);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists((int)articleVM.ArticleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["PublisherId"] = new SelectList(_context.Accounts, "AccountId", "AccountType", updaterId);
        return View(articleVM);
    }

    private bool ArticleExists(int id)
    {
        return (_context.Articles?.Any(e => e.ArticleId == id)).GetValueOrDefault();
    }
}
