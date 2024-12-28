using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnASP.Data;
using DoAnASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DoAnASP.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlaylistsController> _logger;

        public PlaylistsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<PlaylistsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Playlists.Include(p => p.User).Include(p => p.PlaylistSongs);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                         .Include(p => p.PlaylistSongs)
                             .ThenInclude(ps => ps.Song) // Tải thông tin bài hát
                             .ThenInclude(s => s.Album) // Tải thông tin album của bài hát
                         .FirstOrDefaultAsync(m => m.PlaylistID == id);

            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        /* tạo playlist */
        [HttpPost]
        public IActionResult CreatePlaylist()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var applicationDbContext = _context.Playlists.Where(p => p.UserID == userId).ToList();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User is not authenticated.");
            }

            var newPlaylist = new Playlist
            {
                UserID = userId,
                Title = "New Playlist #" + (applicationDbContext.Count + 1 ),
                Description = "Description here",
                IsPublic = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PlaylistSongs = new List<PlaylistSong>()
            };

            _context.Playlists.Add(newPlaylist);
            _context.SaveChanges();
            newPlaylist = _context.Playlists.Where(s => s.Title == newPlaylist.Title).FirstOrDefault();

            var responseData = new
            {
                PlaylistID = newPlaylist.PlaylistID,
                Title = newPlaylist.Title,
                Description = newPlaylist.Description,
                IsPublic = newPlaylist.IsPublic,
                CreatedAt = newPlaylist.CreatedAt,
                UpdatedAt = newPlaylist.UpdatedAt,
                UserName = User.Identity.Name,
                PlaylistSongs = newPlaylist.PlaylistSongs
            };
            _logger.LogError("Respone: " + responseData.ToString());
            return Json(responseData);
        }



        // GET: Playlists/Create
        public IActionResult Create()
        {
            var users = _context.Users.ToList();  // Sử dụng _context.Users thay vì _context.Set<ApplicationUser>()
            ViewData["UserID"] = new SelectList(users, "Id", "UserName");  // Chỉnh sửa này giúp tránh lỗi NullReferenceException
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaylistID,UserID,Title,Description,IsPublic,CreatedAt,UpdatedAt")] Playlist playlist)
        {
            _logger.LogError("Playlistid----:" + playlist.PlaylistID);
            ModelState.Remove("User");
            ModelState.Remove("PlaylistSongs");

            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("Da co loi !!!");
            var users = _context.Users.ToList();  // Sử dụng _context.Users thay vì _context.Set<ApplicationUser>()
            ViewData["UserID"] = new SelectList(users, "Id", "UserName");  // Chỉnh sửa này giúp tránh lỗi NullReferenceException
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            var users = _context.Users.ToList();  // Sử dụng _context.Users thay vì _context.Set<ApplicationUser>()
            ViewData["UserID"] = new SelectList(users, "Id", "UserName");  // Chỉnh sửa này giúp tránh lỗi NullReferenceException
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlaylistID,UserID,Title,Description,IsPublic,CreatedAt,UpdatedAt")] Playlist playlist)
        {

            ModelState.Remove("User");
            ModelState.Remove("PlaylistSongs");


            if (id != playlist.PlaylistID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.PlaylistID))
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
            var users = _context.Users.ToList();  // Sử dụng _context.Users thay vì _context.Set<ApplicationUser>()
            ViewData["UserID"] = new SelectList(users, "Id", "UserName");  // Chỉnh sửa này giúp tránh lỗi NullReferenceException
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PlaylistID == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlists.Any(e => e.PlaylistID == id);
        }
    }
}
