using System;

namespace iFanfics.BLL.DTO {
    public class FullInfoFanfic {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string GenreName { get; set; }
        public string ApplicationUserId { get; set; }
        public string GenreId { get; set; }
    }
}
