using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPortal.Models;

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
}
