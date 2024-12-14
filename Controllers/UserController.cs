using HostelManagement.Data;
using HostelManagement.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HostelManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View(new User());

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = HashPassword(user.Password);

                await _context.Users.InsertOneAsync(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user != null && VerifyPassword(password, user.Password))
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("FullName", $"{user.FirstName} {user.LastName}");
                HttpContext.Session.SetString("Role", user.Role);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashedPassword}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedPasswordHash = parts[1];

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword == storedPasswordHash;
        }
    }
}
