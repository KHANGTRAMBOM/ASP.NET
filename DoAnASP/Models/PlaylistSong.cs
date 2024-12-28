using System.ComponentModel;

namespace DoAnASP.Models
{
    public class PlaylistSong
    {
        [DisplayName("Playlist")]
        public int PlaylistID { get; set; } // Foreign Key
        [DisplayName("Bài hát")]
        public int SongID { get; set; } // Foreign Key
        [DisplayName("Vị trí")]
        public int Position { get; set; }

        [DisplayName("Thêm vào lúc")]
        public DateTime AddedAt { get; set; }

        // Navigation Properties
        public Playlist Playlist { get; set; }

        [DisplayName("Bài hát")]
        public Song Song { get; set; }
    }

}
