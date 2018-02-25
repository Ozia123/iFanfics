using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFanfics.DAL.Entities {
    public class FanficTags {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string FanficId { get; set; }
        public string TagId { get; set; }
    }
}
