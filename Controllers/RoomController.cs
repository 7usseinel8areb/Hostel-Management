using HostelManagement.Data;
using HostelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HostelManagement.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var rooms = await _context.Rooms.Find(_ => true).ToListAsync();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new Room());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                await _context.Rooms.InsertOneAsync(room);
                return RedirectToAction("Index");
            }
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var room = await _context.Rooms.Find(r => r.Id == new ObjectId(id)).FirstOrDefaultAsync();
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Room room)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id != room.Id.ToString())
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _context.Rooms.ReplaceOneAsync(r => r.Id == room.Id, room);
                return RedirectToAction("Index");
            }
            return View(room);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            await _context.Rooms.DeleteOneAsync(r => r.Id == new ObjectId(id));
            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            var userRole = HttpContext.Session.GetString("Role");
            return userRole == "Admin";
        }
    }
}
