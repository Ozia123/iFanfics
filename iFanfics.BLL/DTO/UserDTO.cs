using System;

namespace iFanfics.BLL.DTO {
    public class UserDTO {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string PictureURL { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Role { get; set; }
    }
}