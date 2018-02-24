using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.Web.Models {
    public class CreateFanfic {
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        public string pictureUrl { get; set; }
    }
}