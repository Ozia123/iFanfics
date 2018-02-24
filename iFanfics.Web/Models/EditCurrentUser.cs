using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class EditCurrentUser {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "You must upload profile picture")]
        public string PictureURL { get; set; }
    }
}
