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
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Likes.Include(l => l.Song).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(string userId, int songId)
        {
            
            if (userId == null || songId == 0)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Song)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.UserID == userId && m.SongID == songId);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }


        // GET: Likes/Create
        public IActionResult Create()
        {
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "Title");
            ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName");
            return View();
        }

        // POST: Likes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,SongID,CreatedAt")] Like like)
        {
            ModelState.Remove("User");
            ModelState.Remove("Song");

            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "Title", like.SongID);
            ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", like.UserID);
            return View(like);
        }

        // GET: Likes/Edit/5
        public async Task<IActionResult> Edit(string userId, int songId)
        {
            if (userId == null || songId == 0)
            {
                return NotFound();
            }

            var like = await _context.Likes.FindAsync(userId, songId); // Lưu ý sửa FindAsync
            if (like == null)
            {
                return NotFound();
            }
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "Title", like.SongID);
            ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", like.UserID);
            return View(like);
        }


        // POST: Likes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, int songId, [Bind("UserID,SongID,CreatedAt")] Like like)
        {
            ModelState.Remove("User");
            ModelState.Remove("Song");

            // Kiểm tra nếu UserID hoặc SongID không khớp
            if (userId != like.UserID || songId != like.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(like);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(like.UserID, like.SongID))
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
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "Title", like.SongID);
            ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", like.UserID);
            return View(like);
        }


        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(string userId, int songId)
        {
            if (userId == null || songId == 0)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Song)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.UserID == userId && m.SongID == songId);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }


        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId, int songId)
        {
            var like = await _context.Likes.FindAsync(userId, songId);
            if (like != null)
            {
                _context.Likes.Remove(like);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(string userId, int songId)
        {
            return _context.Likes.Any(e => e.UserID == userId && e.SongID == songId);
        }

    }
}
