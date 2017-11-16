using System.Web;

namespace SavePicture.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string ImgTitle { get; set; }
        public byte[] ImgByte { get; set; }
        public string ImgPath { get; set; }
      
    }
}