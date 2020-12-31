using AlignGalleryCore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignGalleryCore
{
    public class Cache
    {
        public List<Photo> OriginalPhotos { get; set; }

        public List<Photo> Photos { get; set; }

        public int Pointer { get; set; }

        public List<Photo> GetPhotos(int amount)
        {
           

            return Photos.Take(amount).ToList();
        }



    }
}
