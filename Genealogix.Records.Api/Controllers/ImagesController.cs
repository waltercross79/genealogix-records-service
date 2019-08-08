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
    public class ImagesController : ControllerBase
    {        
        readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;    
        }
        
        // GET api/images/5

        /// <summary>
        /// Gets image with given id from the image data store.
        /// </summary>
        /// <param name="id">Unique identifier of the image.</param>
        /// <returns>Binary content of the image.</returns>
        [HttpGet("{id}")]
        public ActionResult<byte[]> Get(string id)
        {
            throw new NotImplementedException();
        }

        // POST api/images

        /// <summary>
        /// Saves image in the image store and generates unique identifier.
        /// </summary>
        /// <param name="image">Binary representation of the image.</param>
        [HttpPost]
        public ActionResult<string> Post([FromForm] byte[] image)
        {
            throw new NotImplementedException();
        }

        // DELETE api/images/5

        /// <summary>
        /// Deletes image from the image store.
        /// </summary>
        /// <param name="id">Unique identifier of the image to remove.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/images/thumbnails?id=abcd&id=efgh

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
