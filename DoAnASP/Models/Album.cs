using System.ComponentModel.DataAnnotations;

namespace DoAnASP.Models
{
    public class Album
    {
        [Display(Name = "Tên Albums")]
        public int AlbumID { get; set; } // Primary Key

        [Display(Name = "Tên nghệ sĩ")]
        public int ArtistID { get; set; } // Foreign Key

        [Display(Name = "Tên Albums")]
        public string Title { get; set; }

        [Display(Name = "Ảnh nền")]
        public string CoverArt { get; set; }

        [Display(Name = "Ngày xuất bản")]
        public DateTime ReleaseDate { get; set; }
        public int GenreID { get; set; } // Foreign Key

        // Navigation Properties
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
