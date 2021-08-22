using System.Drawing;

namespace Flow.Image
{
    public class EmptyPicture : Picture
    {
        public EmptyPicture() : base(0, 0) { }

        public override Bitmap ToBitmap()
            => null;
    }
}