using System.Drawing;

namespace Flow.Image.Processing
{
    public class IntegralPicture : BasePicture
    {
        public BasePicture BasePicture { get; set; }

        
        public override Bitmap ToBitmap()
            => BasePicture.ToBitmap();
    }
}