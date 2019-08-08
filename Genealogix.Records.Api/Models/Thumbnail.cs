namespace Genealogix.Records.Api.Models 
{
    public class Thumbnail
    {
        /// <summary>
        /// Thumbnail image binary.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Unique identifier of the thumbnail.
        /// </summary>
        public string ID { get; set; }        
    }
}