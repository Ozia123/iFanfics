using System.Collections.Generic;

namespace iFanfics.DAL.Entities {
    public class Genre {
        public string Id { get; set; }
        public string GenreName { get; set; }

        public virtual List<Fanfic> Fanfics { get; set; }
    }
}
