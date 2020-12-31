using AlignGalleryCore.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cache
{
    public interface ICacheService
    {
        public Task<List<Photo>> GetGalleryPhotosAsync(int amount);

        public Task InitCache();
    }
}
