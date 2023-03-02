using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE.jsonServer
{
    /// <summary>
    /// Tag is a class of the result from the taggs by the imaggga
    /// </summary>
    public class Tag
    {
        public double confidence { get; set; }
        public Tag2 tag { get; set; }
    }
}
