using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFanfics.DAL.Entities {
    public class ClientProfile {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string PictureURL { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}