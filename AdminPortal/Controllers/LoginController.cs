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

        // GET: Login
        public async Task<IActionResult> Index()
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
                    Console.WriteLine("Email is in use");
                    return NotFound();
                }

                var usernameInUse = _context.Accounts.FirstOrDefault(x => x.Username == createLogin.Username);
                if(usernameInUse != null){
                    Console.WriteLine("Name is in use");
                    return NotFound();
                }

                if(createLogin.Password != createLogin.PasswordConfirm){
                    Console.WriteLine("Password no match");
                    return NotFound();
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

        public IActionResult Login(){ return View(); }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM){
            // check if loginID exists
            var account = _context.Accounts.FirstOrDefault(x => loginVM.Email == x.Email);
            if (account == null){
                Console.WriteLine("Account Not Found");
                return View();
            }

            var login = _context.Logins.FirstOrDefault(x => x.AccountId == account.AccountId);
            // match passwordhash and return error if no match
            var passwordMatch = hasher.VerifyHashedPassword(account, login.PasswordHash, loginVM.Password);
            if (login == null || string.IsNullOrEmpty(loginVM.Password) || passwordMatch != PasswordVerificationResult.Success)
            {
              ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
              Console.WriteLine("Login failed!");
            //   return View(new LoginVM { Email = account.Email });
              return View();
            }
            Console.WriteLine("Login success!");
            return RedirectToAction("Index");
        }

        private bool LoginExists(int id)
        {
          return (_context.Logins?.Any(e => e.LoginId == id)).GetValueOrDefault();
        }

    }
}
