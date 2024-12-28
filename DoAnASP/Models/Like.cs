using System.ComponentModel;

namespace DoAnASP.Models
{
    public class Like
    {
        [DisplayName("Tên người dùng")]
        public string UserID { get; set; } // Foreign Key

        [DisplayName("Tên bài hát")]
        public int SongID { get; set; } // Foreign Key

        [DisplayName("Vào lúc")]
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        [DisplayName("Tên người dùng")]
        public ApplicationUser User { get; set; }

        [DisplayName("Tên bài hát")]
        public Song Song { get; set; }
    }

}
