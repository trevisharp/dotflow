using System.Drawing;
using System.Drawing.Imaging;

namespace Flow.Image
{
    using Processing;
    public abstract class BasePicture
    {
        private static Picture empty = new EmptyPicture();
        public static Picture Empty => empty;
        public void Save(string path)
        {
            if (this is Picture pic)
                pic.bmp.Save(path);
            else if (this is ByteGrayscaleProcessingPicture bgspp)
                bgspp.Close().Save(path);
            else if (this is ByteProcessingPicture bpp)
                bpp.Close().Save(path);
            else if (this is FloatProcessingPicture fpp)
                fpp.Close().Save(path);
            else if (this is FloatGrayScaleProcessingPicture fgspp)
                fgspp.Close().Save(path);
        }
        public abstract Bitmap ToBitmap();
        public static Picture New(string path)
            => new Picture(path);
        public static Picture New(int width, int height)
            => new Picture(width, height);
        public static Picture New(Bitmap bmp)
            => new Picture(bmp);
        public Picture Copy()
        {
            Picture bmppic = null;
            if (this is Picture pic)
                bmppic = pic;
            else if (this is ByteGrayscaleProcessingPicture bgspp)
                bmppic = bgspp.Close();
            else if (this is ByteProcessingPicture bpp)
                bmppic = bpp.Close();
            else if (this is FloatProcessingPicture fpp)
                bmppic = fpp.Close();
            else if (this is FloatGrayScaleProcessingPicture fgspp)
                bmppic = fgspp.Close();

            var newbmp = bmppic.bmp.Clone(bmppic.rect, PixelFormat.Format24bppRgb) as Bitmap;
            if (newbmp is null)
                return null;
            return new Picture(newbmp);
        }
    }
}