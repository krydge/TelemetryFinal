using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore {
    public class Entity {

        public String EntityText { get; set; }
        public String Type { get; set; }
        public Byte? StartIndex { get; set; }
        public Byte? EndIndex { get; set; }
        public DateTime? DateTime { get; set; }

        public Entity() {
            StartIndex = null;
            EndIndex = null;
            DateTime = null;
        }
    }
}
