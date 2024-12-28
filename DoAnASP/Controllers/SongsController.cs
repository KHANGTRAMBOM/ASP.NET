using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnASP.Data;
using DoAnASP.Models;
using Mono.TextTemplating;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace DoAnASP.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SongsController> _logger;
      
        public SongsController(ApplicationDbContext context, ILogger<SongsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Songs.Include(s => s.Album).Include(s => s.Artist).Include(s => s.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Title");
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongID,AlbumID,ArtistID,GenreID,Title,AudioFile,Duration,PlayCount,IsExplicit,Image")] Song song, [Bind] IFormFile AudioFileUpLoad, [Bind] IFormFile ImageUpLoad)
        {

            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Title", song.AlbumID);
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name", song.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name", song.GenreID);

            if (AudioFileUpLoad != null && AudioFileUpLoad.Length > 0)
            {
                // Lấy tên file
                var audioFileName = Path.GetFileName(AudioFileUpLoad.FileName);
                // Đường dẫn lưu file
                var audioPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\audios", audioFileName);

                // Sao chép file
                using (var stream = new FileStream(audioPath, FileMode.Create))
                {
                    await AudioFileUpLoad.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                song.AudioFile = audioFileName;

            }
            else
            {
                ModelState.AddModelError("AudioFile", "Vui lòng tải lên file âm thanh.");
                return View();
            }

            if (ImageUpLoad != null && ImageUpLoad.Length > 0)
            {
                // Lấy tên file
                var imageFileName = Path.GetFileName(ImageUpLoad.FileName);
                // Đường dẫn lưu file
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\pictures", imageFileName);

                // Sao chép file
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageUpLoad.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                song.Image = imageFileName;
            }
            else
            {
                ModelState.AddModelError("Image", "Vui lòng tải lên ảnh bìa.");
                return View();
            }



            ModelState.Remove("Album");
            ModelState.Remove("Artist");
            ModelState.Remove("Genre");
            ModelState.Remove("Likes");
            ModelState.Remove("Comments");
            ModelState.Remove("PlaylistSongs");

            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Title", song.AlbumID);
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name", song.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name", song.GenreID);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongID,AlbumID,ArtistID,GenreID,Title,AudioFile,Duration,PlayCount,IsExplicit,Image")] Song song, [Bind] IFormFile AudioFileUpLoad, [Bind] IFormFile ImageUpLoad)
        {

            var current_song = _context.Songs.AsNoTracking().FirstOrDefault(s => s.SongID == id);

            if (song.ArtistID == null)
            {
                song.ArtistID = current_song.ArtistID;
            }

            if (song.AlbumID == null)
            {
                song.AlbumID = current_song.AlbumID;
            }

            if (song.GenreID == null)
            {
                song.GenreID = current_song.GenreID;
            }


            if (AudioFileUpLoad != null && AudioFileUpLoad.Length > 0)
            {
                // Lấy tên file
                var audioFileName = Path.GetFileName(AudioFileUpLoad.FileName);
                // Đường dẫn lưu file
                var audioPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\audios", audioFileName);

                // Sao chép file
                using (var stream = new FileStream(audioPath, FileMode.Create))
                {
                    await AudioFileUpLoad.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                song.AudioFile = audioFileName;

            }



            if (ImageUpLoad != null && ImageUpLoad.Length > 0)
            {
                // Lấy tên file
                var imageFileName = Path.GetFileName(ImageUpLoad.FileName);
                // Đường dẫn lưu file
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\pictures", imageFileName);

                // Sao chép file
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageUpLoad.CopyToAsync(stream);
                }

                // Lưu tên file vào model
                song.Image = imageFileName;
            }

            ModelState.Remove("ImageUpLoad");
            ModelState.Remove("AudioFileUpLoad");
            ModelState.Remove("Album");
            ModelState.Remove("Artist");
            ModelState.Remove("Genre");
            ModelState.Remove("Likes");
            ModelState.Remove("Comments");
            ModelState.Remove("PlaylistSongs");


            if (id != song.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongID))
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


            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Title", song.AlbumID);
            ViewData["ArtistID"] = new SelectList(_context.Artist, "Id", "Name", song.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "GenreID", "Name", song.GenreID);

            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongID == id);
        }
    }
}
