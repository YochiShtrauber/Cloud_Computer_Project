using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE.jsonServer
{ 
        /// <summary>
        /// Root is a class of the result from the taggs by the imaggga
        /// </summary>
    public class Root
    {
        public Result result { get; set; }
        public Status status { get; set; }
    }
}
