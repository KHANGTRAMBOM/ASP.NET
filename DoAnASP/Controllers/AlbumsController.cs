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
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumID,ArtistID,Title,CoverArt,ReleaseDate,GenreID")] Album album ,[Bind]IFormFile Image)
        {
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name", album.GenreID);

            if (Image != null && Image.Length > 0)
            {
                // Lấy tên file
                var imageFileName = Path.GetFileName(Image.FileName);
                // Đường dẫn lưu file
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\coverarts", imageFileName);

                // Sao chép file
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                album.CoverArt = imageFileName;
            }
            else
            {
                ModelState.AddModelError("CoverArt", "Vui lòng tải lên ảnh bìa.");
                return View();
            }

            ModelState.Remove("Artist");
            ModelState.Remove("Genre");
            ModelState.Remove("Songs");

            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
     
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,ArtistID,Title,CoverArt,ReleaseDate,GenreID")] Album album,IFormFile Image)
        {

            if (Image != null && Image.Length > 0)
            {
                // Lấy tên file
                var imageFileName = Path.GetFileName(Image.FileName);
                // Đường dẫn lưu file
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\coverarts", imageFileName);

                // Sao chép file
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                album.CoverArt = imageFileName;
            }

            ModelState.Remove("Image");
            ModelState.Remove("Artist");
            ModelState.Remove("Genre");
            ModelState.Remove("Songs");

            if (id != album.AlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
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

            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name", album.GenreID);

            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumID == id);
        }
    }
}
