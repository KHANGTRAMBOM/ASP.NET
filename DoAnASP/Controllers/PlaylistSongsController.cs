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
    public class PlaylistSongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaylistSongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlaylistSongs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlaylistSongs
                                        .Include(p => p.Playlist)
                                        .Include(p => p.Song)
                                        .OrderBy(p => p.PlaylistID)  // Sắp xếp theo PlaylistID
                                        .ThenBy(p => p.Position);   // Sau đó sắp xếp theo Position

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlaylistSongs/Details/5
        public async Task<IActionResult> Details(int playlistId, int songId)
        {
            if (playlistId == 0 || songId == 0)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSongs
                .Include(ps => ps.Playlist)
                .Include(ps => ps.Song)
                .FirstOrDefaultAsync(ps => ps.PlaylistID == playlistId && ps.SongID == songId);

            if (playlistSong == null)
            {
                return NotFound();
            }

            return View(playlistSong);
        }


        // GET: PlaylistSongs/Create
        public IActionResult Create()
        {
            ViewData["PlaylistID"] = new SelectList(_context.Playlists, "PlaylistID", "Title");
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "Title");

            // Tạo một đối tượng để lưu số lượng bài hát theo PlaylistID
            ViewBag.SongCountPerPlaylist = _context.PlaylistSongs
                .GroupBy(ps => ps.PlaylistID)
                .ToDictionary(g => g.Key, g => g.Count());


            return View();
        }

        // POST: PlaylistSongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaylistID,SongID,Position,AddedAt")] PlaylistSong playlistSong)
        {
            ModelState.Remove("Playlist");
            ModelState.Remove("Song");

            if (ModelState.IsValid)
            {
                _context.Add(playlistSong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistID"] = new SelectList(_context.Playlists, "PlaylistID", "PlaylistID", playlistSong.PlaylistID);
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "SongID", playlistSong.SongID);
            return View(playlistSong);
        }

        // GET: PlaylistSongs/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int playlistId, int songId)
        {
            var playlistSong = await _context.PlaylistSongs
                .FirstOrDefaultAsync(ps => ps.PlaylistID == playlistId && ps.SongID == songId);

            if (playlistSong == null)
            {
                return NotFound();
            }

            ViewData["PlaylistID"] = new SelectList(_context.Playlists, "PlaylistID", "Title", playlistSong.PlaylistID);
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "Title", playlistSong.SongID);

            return View(playlistSong);
        }



        // POST: PlaylistSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int playlistId, int songId, [Bind("PlaylistID,SongID,Position,AddedAt")] PlaylistSong playlistSong)
        {

            ModelState.Remove("Playlist");
            ModelState.Remove("Song");

            if (playlistId != playlistSong.PlaylistID || songId != playlistSong.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlistSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistSongExists(playlistSong.PlaylistID, playlistSong.SongID))
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
            ViewData["PlaylistID"] = new SelectList(_context.Playlists, "PlaylistID", "PlaylistID", playlistSong.PlaylistID);
            ViewData["SongID"] = new SelectList(_context.Songs, "SongID", "SongID", playlistSong.SongID);
            return View(playlistSong);
        }

        private bool PlaylistSongExists(int playlistId, int songId)
        {
            return _context.PlaylistSongs.Any(e => e.PlaylistID == playlistId && e.SongID == songId);
        }


        // GET: PlaylistSongs/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int playlistId, int songId)
        {
            if (playlistId == 0 || songId == 0)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSongs
             .Include(ps => ps.Playlist)
             .Include(ps => ps.Song)
             .FirstOrDefaultAsync(ps => ps.PlaylistID == playlistId && ps.SongID == songId);

            if (playlistSong == null)
            {
                return NotFound();
            }

            return View(playlistSong);
        }

        // POST: PlaylistSongs/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int PlaylistID, int SongID)
        {
            var playlistSong = await _context.PlaylistSongs
                .FirstOrDefaultAsync(ps => ps.PlaylistID == PlaylistID && ps.SongID == SongID);

            if (playlistSong != null)
            {
                _context.PlaylistSongs.Remove(playlistSong);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
