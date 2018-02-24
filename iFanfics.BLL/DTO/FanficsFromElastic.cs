using System;
using System.Collections.Generic;
using System.Text;

namespace iFanfics.BLL.DTO {
    public class FanficsFromElastic {
        public List<FullInfoFanfic> Data { get; set; }
        public int Total { get; set; }

        public FanficsFromElastic() {
            Data = new List<FullInfoFanfic>();
        }
    }
}
