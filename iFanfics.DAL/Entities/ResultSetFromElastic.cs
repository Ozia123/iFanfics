using System.Collections.Generic;

namespace iFanfics.DAL.Entities {
    public class ResultSetFromElastic {
        public List<FanficForElastic> Data { get; set; }
        public int Total { get; set; }

        public ResultSetFromElastic() {
            Data = new List<FanficForElastic>();
        }
    }
}
