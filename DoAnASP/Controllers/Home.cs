using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnASP.Data;
using DoAnASP.Models;
using System.Text.Json;
using DoAnASP.Areas.Identity.Pages.Account;

namespace DoAnASP.Controllers
{
    public class SongsAndAlbumsViewModel
    {
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Playlist> Playlists { get; set; }
        public IEnumerable<Like> Likes { get; set; }

    }

    public class CustomModel2
    {
        public Song song;
        public IEnumerable<Playlist> Playlists { get; set; }

        public IEnumerable<Like> Likes { get; set; }
    }


    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SongsController> _logger;
        public HomeController(ApplicationDbContext context, ILogger<SongsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /*Like*/
        [HttpPost]
        public IActionResult ToggleLike(int songId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "User is not authenticated." });
            }

            var existingLike = _context.Likes.FirstOrDefault(l => l.UserID == userId && l.SongID == songId);

            if (existingLike == null)
            {
                // Add new like
                var newLike = new Like
                {
                    UserID = userId,
                    SongID = songId,
                    CreatedAt = DateTime.Now
                };
                _context.Likes.Add(newLike);
                _context.SaveChanges();
                return Json(new { success = true, liked = true });
            }
            else
            {
                // Remove existing like
                _context.Likes.Remove(existingLike);
                _context.SaveChanges();
                return Json(new { success = true, liked = false });
            }
        }



        [HttpGet]
        public IActionResult AddPlaylistSong(int playlistId, int songId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest("User is not authenticated.");

            var playlist = _context.Playlists
                .Include(p => p.PlaylistSongs)
                .FirstOrDefault(p => p.PlaylistID == playlistId && p.UserID == userId);

            if (playlist == null)
                return NotFound("Playlist not found or unauthorized.");

            var song = _context.Songs.Find(songId);
            if (song == null)
                return NotFound("Song not found.");

            if (playlist.PlaylistSongs.Any(ps => ps.SongID == songId))
            {
                TempData["Message"] = "Đã tồn tại rồi!";
                return RedirectToAction("Index");
            }

            playlist.PlaylistSongs.Add(new PlaylistSong
            {
                PlaylistID = playlistId,
                SongID = songId,
                Position = playlist.PlaylistSongs.Count + 1,
                AddedAt = DateTime.Now
            });

            _context.SaveChanges();

            // Redirect or return JSON based on your UI requirement
            return RedirectToAction("Index"); // Replace "Index" with your target action
        }




        //Xóa playlist

        public async Task<IActionResult> Delete2(int? id)
        {
            var playlist = await _context.Playlists.FindAsync(id);

            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Playlists/Details/5   Lấy detail của playlist
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                         .Include(p => p.User) // Tải thông tin người dùng
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


        // GET: Prenium
        public async Task<IActionResult> premium()
        {
           
            return View();
        }



        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var songs = await _context.Songs
                        .Include(s => s.Album)
                        .Include(s => s.Artist)
                        .Include(s => s.Genre)
                        .Include(s => s.Likes)
                        .ToListAsync();

            var playlists = await _context.Playlists
                            .Include(s => s.User)
                            .Include(s => s.PlaylistSongs)
                            .ToListAsync(); // Lấy toàn bộ dữ liệu Album


            var likes = await _context.Likes
                           .Include(s => s.User)
                           .Include(s => s.Song).ThenInclude(s => s.Artist)
                           .ToListAsync(); // Lấy toàn bộ dữ liệu Album

            // Tạo ViewModel
            var viewModel = new SongsAndAlbumsViewModel
            {
                Songs = songs,
                Playlists = playlists,
                Likes = likes
            };

            return View(viewModel);
        }


        private const string LastPlayedKey = "LastPlayedSong";

        // Bài hát nghe gần nhất
        public void SaveLastPlayed(string songId, string title, string artist, string audioFile, string image)
        {
            var lastPlayed = new
            {
                SongId = songId,
                Title = title,
                Artist = artist,
                AudioFile = audioFile,
                Image = image,
                Timestamp = DateTime.Now
            };

            // Convert to JSON and store in session
            HttpContext.Session.SetString(LastPlayedKey, JsonSerializer.Serialize(lastPlayed));
        }

        public IActionResult GetLastPlayed()
        {
            var lastPlayedJson = HttpContext.Session.GetString(LastPlayedKey);
            return Json(lastPlayedJson != null ? JsonSerializer.Deserialize<object>(lastPlayedJson) : null);
        }


        // Tìm kiếm theo tên nhạc
        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Json(new { success = false, message = "Query cannot be empty" });
            }

            // Normalize the query
            query = query.Trim().ToLower();

            var results = _context.Songs
                .AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Where(s => EF.Functions.Like(s.Title.ToLower(), $"%{query}%") ||
                            EF.Functions.Like(s.Artist.Name.ToLower(), $"%{query}%"))
                .Select(s => new
                {
                    songID = s.SongID,
                    title = s.Title,
                    artistName = s.Artist != null ? s.Artist.Name : "Unknown Artist",
                    image = s.Image,
                    audioFile = s.AudioFile,
                    albumName = s.Album != null ? s.Album.Title : "Unknown Album"
                })
                .ToList();

            return Json(new { success = true, kq = results });
        }


        [HttpPost("TangLX/{id}")]
        public async Task<IActionResult> TangLX(int id)
        {
            _logger.LogWarning($"Đã vào hàm");

            var song = await _context.Songs.FindAsync(id);


            _logger.LogWarning($"Bài hát là : {song.Title}");

            if (song == null)
            {
                return NotFound();
            }
            

            song.PlayCount++;
            _logger.LogWarning($"Da tang luot nghe: {song.PlayCount}");

            await _context.SaveChangesAsync();
            return Ok(song.PlayCount);
        }



        // GET: Songs
        public async Task<IActionResult> SreachEmpty()
        {
            var applicationDbContext = _context.Songs.Include(s => s.Album).Include(s => s.Artist).Include(s => s.Genre).Include(s => s.Likes);
            
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

            var playlists = await _context.Playlists
                 .Include(s => s.User)
                 .Include(s => s.PlaylistSongs)
                 .ToListAsync(); // Lấy toàn bộ dữ liệu Album

            var likes = await _context.Likes
                .Include(s => s.User)
                .Include(s => s.Song)
                .ToListAsync(); // Lấy toàn bộ dữ liệu Album

            var CustomeModel2 = new CustomModel2();

            CustomeModel2.song = song;
            CustomeModel2.Playlists = playlists;
            CustomeModel2.Likes = likes;
            if (song == null)
            {
                return NotFound();
            }


            return View(CustomeModel2);
        
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
