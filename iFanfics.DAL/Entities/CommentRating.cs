using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFanfics.DAL.Entities {
    public class CommentRating {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string CommentId { get; set; }

        [Required]
        public int GivenRating { get; set; }
    }
}
