using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudApp.Data;
using System.Diagnostics;

namespace CloudApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var users = await _context.Users.ToListAsync();

           
            return View(users);
        }

        
        public IActionResult About()
        {
            return View();
        }

       
        public IActionResult Contact()
        {
            return View();
        }
    }
}
