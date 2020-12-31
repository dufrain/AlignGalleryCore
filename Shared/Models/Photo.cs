using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignGalleryCore.Shared
{
    public class Photo
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Url { get; set; }

        public string Download_url { get; set; }
    }
}
