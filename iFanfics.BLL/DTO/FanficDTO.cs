using System;
using System.Collections.Generic;

namespace iFanfics.BLL.DTO {
    public class FanficDTO {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string GenreId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastModifyingDate { get; set; }
        public List<TagDTO> FanficTags { get; set; }
    }
}
