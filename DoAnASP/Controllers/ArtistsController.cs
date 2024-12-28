using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnASP.Data;
using DoAnASP.Models;

namespace DoAnASP.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistsController> _logger;
        public ArtistsController(ApplicationDbContext context, ILogger<ArtistsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artist.ToListAsync());
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .Include(s => s.Albums)
                .Include(s => s.Songs).ThenInclude(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Bio,Avatar,IsVerified,CreatedAt")] Artist artist ,[Bind]IFormFile AvatarUpLoad)
        {
            if (AvatarUpLoad != null && AvatarUpLoad.Length > 0)
            {
                // Lấy tên file
                var avatarFileName = Path.GetFileName(AvatarUpLoad.FileName);
                // Đường dẫn lưu file
                var avatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\artists", avatarFileName);

                // Sao chép file
                using (var stream = new FileStream(avatarPath, FileMode.Create))
                {
                    await AvatarUpLoad.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                artist.Avatar = avatarFileName;

            }
            else
            {
                ModelState.AddModelError("Avatar", "Vui lòng tải lên ảnh bìa.");
                return View(artist);
            }

            ModelState.Remove("Albums");
            ModelState.Remove("Songs");


            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Bio,Avatar,IsVerified,CreatedAt")] Artist artist, [Bind] IFormFile AvatarUpLoad)
        {

            if (AvatarUpLoad != null && AvatarUpLoad.Length > 0)
            {
                // Lấy tên file
                var avatarFileName = Path.GetFileName(AvatarUpLoad.FileName);
                // Đường dẫn lưu file
                var avatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\artists", avatarFileName);

                // Sao chép file
                using (var stream = new FileStream(avatarPath, FileMode.Create))
                {
                    await AvatarUpLoad.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                artist.Avatar = avatarFileName;
            }

            ModelState.Remove("AvatarUpLoad");
            ModelState.Remove("Songs");
            ModelState.Remove("Albums");


            foreach (var state in ModelState)
            {
                if (state.Value.Errors.Count > 0)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError("Validation Error: Key={Key}, Error={Error}", state.Key, error.ErrorMessage);
                    }
                }
            }

            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
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
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            if (artist != null)
            {
                _context.Artist.Remove(artist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }
    }
}
