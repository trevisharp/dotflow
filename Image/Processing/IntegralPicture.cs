using System.Drawing;

namespace Flow.Image.Processing
{
    public class IntegralPicture : Picture
    {
        public Picture BasePicture { get; set; }

        
        public override Bitmap ToBitmap()
            => BasePicture.ToBitmap();
    }
}