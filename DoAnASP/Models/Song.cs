using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnASP.Models
{
    public class Song
    {
        public int SongID { get; set; } // Primary Key
        public int AlbumID { get; set; } // Foreign Key
        public int ArtistID { get; set; } // Foreign Key
        public int GenreID { get; set; } // Foreign Key
        
        [Display(Name = "Tên bài hát")]
        public string Title { get; set; }
        [Display(Name = "File Audio")]
        public string AudioFile { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Thời lượng")]
        public int Duration { get; set; }
        [Display(Name = "Lượt nghe")]
        public int PlayCount { get; set; }
        [Display(Name = "Giới hạn")]
        public bool IsExplicit { get; set; }

        // Navigation Properties
        [Display(Name = "Tên Album")]
        public Album Album { get; set; }

        [Display(Name = "Nghệ sĩ")]
        public Artist Artist { get; set; }

        [Display(Name = "Thể loại")]
        public Genre Genre { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<PlaylistSong> PlaylistSongs { get; set; }


    }
}
