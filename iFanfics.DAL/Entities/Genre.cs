using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFanfics.DAL.Entities {
    public class Genre {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string GenreName { get; set; }

        public virtual List<Fanfic> Fanfics { get; set; }
    }
}
