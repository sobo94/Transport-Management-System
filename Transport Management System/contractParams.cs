using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2
{
    public enum van { van1, van2 }

    public class contractParams
    {
        public string clientName { get; set; }
        public int jobType { get; set; }
        public int quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public van vanType { get; set; }              
    }
}
