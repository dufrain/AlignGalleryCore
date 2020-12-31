using AlignGalleryCore.Shared;
using Cache;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlignGalleryCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly ICacheService _cache;

        public GalleryController(ICacheService cache)
        {
            _cache = cache;
        }

        // GET: api/<GalleryController>
        [HttpGet("get")]
        public async Task<List<Photo>> Get()
        {
            var photos = await _cache.GetGalleryPhotosAsync(5);
            //var url = "https://picsum.photos/v2/list?page=1&limit=100";
            return photos;
        }

        // GET api/<GalleryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GalleryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GalleryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GalleryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
