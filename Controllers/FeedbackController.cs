using HostelManagement.Data;
using HostelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HostelManagement.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _context.Users.Find(_ => true).ToListAsync();
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.DateSubmitted = DateTime.Now;

                await _context.Feedbacks.InsertOneAsync(feedback);
                return RedirectToAction("Index", "Home");
            }
            return View(feedback);
        }
    }
}
