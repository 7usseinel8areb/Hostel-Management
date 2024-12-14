using HostelManagement.Data;
using HostelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HostelManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }

            var allRooms = await _context.Rooms.Find(_ => true).ToListAsync();

            var allBookings = await _context.Bookings.Find(_ => true).ToListAsync();

            var availableRooms = allRooms.Where(room =>
                !allBookings.Any(booking =>
                    booking.RoomId == room.Id.ToString()
                )).ToList();

            ViewBag.Rooms = availableRooms;

            var booking = new Booking
            {
                UserId = userId
            };

            return View(booking);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }
            booking.UserId = userId;
            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.Find(r => r.Id == new MongoDB.Bson.ObjectId(booking.RoomId)).FirstOrDefaultAsync();

                if (room != null)
                {
                    var duration = (booking.EndDate - booking.StartDate).Days;
                    booking.TotalAmount = duration * room.PricePerNight;
                }

                booking.UserId = userId;

                await _context.Bookings.InsertOneAsync(booking);

                return RedirectToAction("Index", "Home");
            }

            return View(booking);
        }
    }
}
