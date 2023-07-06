using System.Security.Claims;
using AuthorizationService.Data;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Controllers;

public class AccountController : Controller
{
    private readonly AuthorizationDbContext _authorizationDbContext;
    public AccountController(AuthorizationDbContext authorizationDbContext)
    {
        _authorizationDbContext = authorizationDbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        Console.WriteLine(email + password);
        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
        {
            Console.WriteLine("ok");
            return View();
        }
        var user = await _authorizationDbContext.Users.FirstOrDefaultAsync(t => t.Email == email 
            && t.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
            return View();
        }
        Console.WriteLine("no ok");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(
        string username,
        string password,
        string firstname,
        string lastname,
        string email)
    {
        var userIsExist =
            await _authorizationDbContext.Users.FirstOrDefaultAsync(t => t.Email == email || t.Username == username) !=
            null
                ? true
                : false;

        if (!userIsExist)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Email = email,
                Password = password,
                Firstname = firstname,
                Lastname = lastname,
                CreatedAt = DateTime.Now,
                Description = "I am new",
                UpdatedAt = DateTime.Now
            };

            _authorizationDbContext.Users.Add(user);
            await _authorizationDbContext.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        return NotFound();
    }
}