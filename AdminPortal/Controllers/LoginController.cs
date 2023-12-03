using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPortal.Models;
using AdminPortal.ViewModels;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity;

namespace AdminPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly AdminPortalDbContext _context;
        private readonly PasswordHasher<Account> hasher = new PasswordHasher<Account>();

        public LoginController(AdminPortalDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(){ return View(); }

        [HttpPost]
        public IActionResult Index(LoginVM loginVM){
            // check if loginID exists
            var account = _context.Accounts.FirstOrDefault(x => loginVM.Email == x.Email);
            if (account == null){
                ModelState.AddModelError("AccountNotFound", "Account not found, please try again.");
                return View(loginVM);
            }

            var login = _context.Logins.FirstOrDefault(x => x.AccountId == account.AccountId);
            var passwordMatch = hasher.VerifyHashedPassword(account, login.PasswordHash, loginVM.Password);

            if (login == null || string.IsNullOrEmpty(loginVM.Password) || passwordMatch != PasswordVerificationResult.Success)
            {
              ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
              return View(loginVM);
            }

            HttpContext.Session.SetString(nameof(Account.Email), account.Email);
            HttpContext.Session.SetInt32(nameof(Account.AccountId), account.AccountId);
            ViewBag.Test = "test";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Accounts()
        {
            var adminPortalDbContext = _context.Logins.Include(l => l.Account);
            return View(await adminPortalDbContext.ToListAsync());
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountType");
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLoginVM createLogin)
        {
            if (ModelState.IsValid)
            {
                var emailInUse = _context.Accounts.FirstOrDefault(x => x.Email == createLogin.Email);
                if(emailInUse != null){
                    ModelState.AddModelError("EmailInUse", "Email is already in use, please try a different email address.");
                }

                var usernameInUse = _context.Accounts.FirstOrDefault(x => x.Username == createLogin.Username);
                if(usernameInUse != null){
                    ModelState.AddModelError("UsernameInUse", "Username is already in use, please try a different username.");
                }

                if(createLogin.Password != createLogin.PasswordConfirm){
                    ModelState.AddModelError("PasswordNoMatch", "Passwords do not match.");
                }

                if(ModelState.ErrorCount > 0){
                    return View(createLogin);
                }

                Console.WriteLine("Email not in use");

                var newAccount = new Account{
                    Username = createLogin.Username,
                    Email = createLogin.Email,
                    CreationDateUTC = DateTime.UtcNow
                };

                _context.Add(newAccount);
                _context.SaveChanges();

                var account = _context.Accounts.FirstOrDefault(x => x.Email == createLogin.Email);

                var newLogin = new Login{
                    AccountId = account.AccountId,
                    PasswordHash = hasher.HashPassword(newAccount, createLogin.Password)
                };

                _context.Add(newLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
