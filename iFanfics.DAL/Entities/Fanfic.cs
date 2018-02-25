using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFanfics.DAL.Entities {
    public class Fanfic {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string GenreId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string PictureURL { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastModifyingDate { get; set; }

        public virtual List<Chapter> Chapters { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<FanficTags> FanficTags { get; set; }
    }
}
