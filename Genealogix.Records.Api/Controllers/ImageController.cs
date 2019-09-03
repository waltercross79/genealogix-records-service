using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Genealogix.Records.Api.Models;
using Genealogix.Records.Api.Services;

namespace Genealogix.Records.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {        
        readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;    
        }
        
        // GET api/image/5

        /// <summary>
        /// Gets image with given id from the image data store.
        /// </summary>
        /// <param name="id">Unique identifier of the image.</param>
        /// <returns>Binary content of the image.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var image = await _imageService.GetImage(id);
            this.Response.ContentType = image.Item2;

            return new JsonResult(new {value = System.Convert.ToBase64String(image.Item1)});
        }

        // POST api/image

        /// <summary>
        /// Saves image in the image store and generates unique identifier.
        /// </summary>
        /// <param name="image">Binary representation of the image.</param>
        /// <param name="fileName">Original file name.</param>
        [HttpPost]
        [RequestFormLimits(ValueLengthLimit=Int32.MaxValue)]
        public async Task<ActionResult> Post([FromForm] string imageBase64, [FromForm] string fileName)
        {
            byte[] image = System.Convert.FromBase64String(imageBase64);

            string key = await _imageService.SaveImage(fileName, image);

            return new JsonResult(new { value = key });
        }

        // DELETE api/image/5

        /// <summary>
        /// Deletes image from the image store.
        /// </summary>
        /// <param name="id">Unique identifier of the image to remove.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/image/thumbnails?id=abcd&id=efgh

        /// <summary>
        /// Loads thumbnails for all provided identifiers.
        /// </summary>
        /// <param name="id">List of thumbnail identifiers.</param>
        /// <returns>Collection of thumbnail images.</returns>
        [HttpGet("thumbnails")]
        public ActionResult<IEnumerable<Thumbnail>> Thumbnails([FromQuery]string[] id) 
        {
            throw new NotImplementedException();
        }
    }
}
