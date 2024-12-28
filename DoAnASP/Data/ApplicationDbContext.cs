using DoAnASP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnASP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Follow entity
            modelBuilder.Entity<Follow>()
                .HasKey(f => new { f.FollowerID, f.FollowedID }); // Composite key

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Following) // A user follows many users
                .HasForeignKey(f => f.FollowerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Followed)
                .WithMany(u => u.Followers) // A user is followed by many users
                .HasForeignKey(f => f.FollowedID)
                .OnDelete(DeleteBehavior.Restrict);


            // Chỉ định composite key cho entity 'Like'
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserID, l.SongID }); // Chỉ định khóa chính từ hai khóa phụ

            // Nếu cần thiết, cấu hình thêm mối quan hệ với các entity khác
            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes) // Cấu hình mối quan hệ với User
                .HasForeignKey(l => l.UserID);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Song)
                .WithMany(s => s.Likes) // Cấu hình mối quan hệ với Song
                .HasForeignKey(l => l.SongID);



            // Chỉ định composite key cho entity 'PlaylistSong'
            modelBuilder.Entity<PlaylistSong>()
                .HasKey(ps => new { ps.PlaylistID, ps.SongID }); // Chỉ định khóa chính từ hai khóa phụ

            // Cấu hình mối quan hệ với Playlist và Song
            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs) // Cấu hình mối quan hệ với Playlist
                .HasForeignKey(ps => ps.PlaylistID);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs) // Cấu hình mối quan hệ với Song
                .HasForeignKey(ps => ps.SongID);


            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumID)
                .OnDelete(DeleteBehavior.Restrict);


            // Song - Genre
            modelBuilder.Entity<Song>()
                .HasOne(s => s.Genre)
                .WithMany(g => g.Songs)
                .HasForeignKey(s => s.GenreID);

            // Album - Genre
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Genre)
                .WithMany()
                .HasForeignKey(a => a.GenreID);

        }


        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<DoAnASP.Models.Genre> Genre { get; set; } = default!;
        public DbSet<DoAnASP.Models.Artist> Artist { get; set; } = default!;
    }
}
