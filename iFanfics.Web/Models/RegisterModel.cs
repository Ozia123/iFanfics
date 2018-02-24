using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class RegisterModel {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@".{8,}", ErrorMessage = "Invalid password!")]
        public string Password { get; set; }
        public string PictureURL { get; set; }
    }
}
