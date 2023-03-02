using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    /// MyDriveFile is a class of the drive
    /// </summary>
    public class MyDriveFile
    {
        public string Id { get; set; }
        public long? Size { get; set; }
        public string Name { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }
        public IList<string> Parents { get; set; }
        public string MimeType { get; set; }


    }
}
