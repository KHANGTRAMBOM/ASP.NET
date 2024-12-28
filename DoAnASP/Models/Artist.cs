using System.ComponentModel;

namespace DoAnASP.Models
{
    public class Artist
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public bool IsVerified { get; set; }

        [DisplayName("Tạo ngày")]
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<Album> Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
    }

}
