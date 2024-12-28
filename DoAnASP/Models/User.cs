using Microsoft.AspNetCore.Identity;
using System;
namespace DoAnASP.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Các thuộc tính bổ sung
        public string Avatar { get; set; } // Hình đại diện của người dùng
        public DateTime CreatedAt { get; set; } // Ngày tạo tài khoản
        public bool IsPremium { get; set; } // Trạng thái tài khoản Premium
        public DateTime? PremiumExpiry { get; set; } // Ngày hết hạn Premium

        // Navigation Properties
        public ICollection<Playlist> Playlists { get; set; } // Người dùng có thể tạo nhiều Playlist
        public ICollection<Like> Likes { get; set; } // Người dùng có thể thích nhiều bài hát
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Following { get; set; }
    }
}

