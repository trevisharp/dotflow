using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Flow.Image
{
    using Util;
    using Processing;
    public class BitmapPicture : Picture, IDisposable
    {
        internal Rectangle rect;
        internal Bitmap bmp = null;
        internal BitmapPicture mainpic = null;
        public BitmapPicture(int width, int height)
        {
            this.bmp = new Bitmap(width, height);
            this.rect = new Rectangle(0, 0, width, height);
        }
        public BitmapPicture(string path)
        {
            this.bmp = Bitmap.FromFile(path) as Bitmap;
            this.rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        }
        public BitmapPicture(Bitmap bmp)
        {
            this.bmp = bmp;
            this.rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        }
        public BitmapPicture SubPicture(int x, int y, int width, int height)
        {
            var pic = new BitmapPicture(bmp);
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
        public static implicit operator Bitmap(BitmapPicture pic)
            => pic.bmp;
        public static implicit operator BitmapPicture(Bitmap bmp)
            => new BitmapPicture(bmp);
        public static implicit operator BitmapPicture(ByteProcessingPicture pp)
            => pp.Close();
        public static implicit operator BitmapPicture(FloatProcessingPicture pp)  
            => pp.Close();
        public static implicit operator BitmapPicture(ByteGrayscaleProcessingPicture pp)
            => pp.Close();
        public static implicit operator BitmapPicture(FloatGrayScaleProcessingPicture pp)
            => pp.Close();
    }
}