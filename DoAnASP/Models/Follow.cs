using System.ComponentModel;

namespace DoAnASP.Models
{
    public class Follow
    {
        [DisplayName("Người theo dỗi")]
        public string FollowerID { get; set; } // Foreign Key
        [DisplayName("Người theo bị dỗi")]
        public string FollowedID { get; set; } // Foreign Key

        [DisplayName("Vào lúc")]
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        [DisplayName("Người theo dỗi")]
        public ApplicationUser Follower { get; set; }

        [DisplayName("Người theo bị dỗi")]
        public ApplicationUser Followed { get; set; }
    }

}
