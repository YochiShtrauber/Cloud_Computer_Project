using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    /// ImageDetails is a class od the image
    /// </summary>
    public class ImageDetails
    {
        public string ImagePath { get; set; } //string of image path
        /// <summary>
        /// the details of image
        /// </summary>
        public Dictionary<string, double> Details { get; set; }
        /// <summary>
        /// the constractor of image details
        /// </summary>
        /// <param name="ImagePath">image path</param>
        public ImageDetails(string ImagePath) { 
            this.ImagePath = ImagePath;
            Details = new Dictionary<string, double>();
        }
    }
}
