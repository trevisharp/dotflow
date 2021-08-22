using System;
using System.Drawing;

namespace Flow.Image
{
    using Processing;
    public class Picture : BasePicture, IDisposable
    {
        internal Rectangle rect;
        internal Bitmap bmp = null;
        internal Picture mainpic = null;
        public Picture(int width, int height)
        {
            if (width == 0 || height == 0)
                return;
            this.bmp = new Bitmap(width, height);
            this.rect = new Rectangle(0, 0, width, height);
        }
        public Picture(string path)
        {
            this.bmp = Bitmap.FromFile(path) as Bitmap;
            this.rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        }
        public Picture(Bitmap bmp)
        {
            this.bmp = bmp;
            this.rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        }
        public Picture SubPicture(int x, int y, int width, int height)
        {
            var pic = new Picture(bmp);
            pic.rect = new Rectangle(x, y, width, height);
            return pic;
        }
        
        public void Dispose()
        {
            if (mainpic is null)
                bmp.Dispose();
            else mainpic.Dispose();
        }
        public override Bitmap ToBitmap()
            => this.bmp;
        public static implicit operator Bitmap(Picture pic)
            => pic.bmp;
        public static implicit operator Picture(Bitmap bmp)
            => new Picture(bmp);
        public static implicit operator Picture(ByteProcessingPicture pp)
            => pp.Close();
        public static implicit operator Picture(FloatProcessingPicture pp)  
            => pp.Close();
        public static implicit operator Picture(ByteGrayscaleProcessingPicture pp)
            => pp.Close();
        public static implicit operator Picture(FloatGrayScaleProcessingPicture pp)
            => pp.Close();
    }
}