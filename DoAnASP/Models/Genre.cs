using System.ComponentModel;

namespace DoAnASP.Models
{
    public class Genre
    {
        public int GenreID { get; set; } // Primary Key

        [DisplayName("Tên Thể Loại")]
        public string Name { get; set; }

        // Navigation Properties
        public ICollection<Song> Songs { get; set; }
    }
}
