using System.ComponentModel;

namespace DoAnASP.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; } // Primary Key

        [DisplayName("Tên người dùng")]
        public string UserID { get; set; } // Foreign Key


        [DisplayName("Tên Playlist")]
        public string Title { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Công khai")]
        public bool IsPublic { get; set; }

        [DisplayName("Tạo lúc")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Cập nhật lúc")]
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties

        [DisplayName("Người dùng")]
        public ApplicationUser User { get; set; }
        public ICollection<PlaylistSong> PlaylistSongs { get; set; }
    }

}
