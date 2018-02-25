using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFanfics.DAL.Entities {
    public class ChapterRating {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ChapterId { get; set; }
        public string ApplicationUserId { get; set; }

        public int GivenRating { get; set; }
    }
}