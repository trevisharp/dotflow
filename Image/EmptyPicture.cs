using System.Drawing;

namespace Flow.Image
{
    public class EmptyPicture : Picture
    { 
        public override Bitmap ToBitmap()
            => null;
    }
}