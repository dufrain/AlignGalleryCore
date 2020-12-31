using AlignGalleryCore.Shared;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cache
{
    public class CacheService : ICacheService
    {
        private const int maxPhotos = 100;
        private const string photosKey = "photosKey";

        private readonly IMemoryCache _cache;
        private readonly HttpClient _client;
        private readonly string url = $"https://picsum.photos/v2/list?page=1&limit={maxPhotos}";

        public CacheService(IMemoryCache cache, HttpClient client)
        {
            _cache = cache;
            _client = client;
        }

        public async Task<List<Photo>> GetGalleryPhotosAsync(int amount)
        {
            if (!_cache.TryGetValue(photosKey, out Dictionary<Photo,bool> photosDict)) 
            {
                await InitCache();
                photosDict = _cache.Get<Dictionary<Photo, bool>>(photosKey);
            }

            if (amount >= maxPhotos)
            {
                throw new Exception($"amount can't be more than {maxPhotos}");
            }

            return GetRandomList(amount, photosDict);
        }

        private List<Photo> GetRandomList(int amount, Dictionary<Photo,bool> photosDict) 
        {
            int index = 0;
            var photosList = new List<Photo>();
            try
            {
                if (photosDict.Where(el => el.Value).ToList().Count < amount)
                {
                    _cache.Set(photosKey, photosDict.ToDictionary(key => key, value => true));
                }

                var currentPhotos = new List<Photo>();
                for (int i = 0; i < amount; i++)
                {
                    Random rand = new Random();
                    photosList = photosDict.Where(el => el.Value).Select(el => el.Key).ToList();
                    index = rand.Next(0, photosList.Count());
                    var randomPhoto = photosList[index];
                    currentPhotos.Add(randomPhoto);
                    photosDict[photosList[index]] = false;
                }
                _cache.Set(photosKey, photosDict);
                return currentPhotos;
            }
            catch (Exception e)
            {
                throw new Exception($"index {index}, photosList.count()= {photosList.Count()}", e);
            }
            
        }

        public async Task InitCache()
        {
            var photos = await FetchPhotos();
            _cache.Set(photosKey, photos.ToDictionary(key => key, value => true));
        }

        private async Task<List<Photo>> FetchPhotos() {
            var httpResponse = await _client.GetAsync($"{url}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve photos"); 
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var photos = JsonConvert.DeserializeObject<List<Photo>>(content);

            return photos;
        }
    }
}
