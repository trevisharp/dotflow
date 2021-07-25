using System;
using System.Drawing;

namespace Sharp.Image
{
    using Util;
    using Processing;
    public class Picture : IDisposable
    {
        internal Rectangle rect;
        internal Bitmap bmp = null;
        internal Picture mainpic = null;
        public Picture(int width, int height)
        {
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

        public void Save(string path)
        {
            bmp.Save(path);
        }
        public void Show()
        {
            if (mainpic != null)
            {
                var bmp = new Bitmap(rect.Width, rect.Height);
                var g = Graphics.FromImage(bmp);
                g.DrawImage(this.bmp, new Rectangle(0, 0, rect.Width, rect.Height), this.rect, GraphicsUnit.Pixel);
                FormBuilder.Builder
                    .SetPicture(bmp)
                    .Show();
            }
            else
            {
                FormBuilder.Builder
                    .SetPicture(this)
                    .Show();
            }
        }

        public void Dispose()
        {
            if (mainpic is null)
                bmp.Dispose();
            else mainpic.Dispose();
        }

        public static Picture New(string path)
            => new Picture(path);
        public static Picture New(int width, int height)
            => new Picture(width, height);
        public static Picture New(Bitmap bmp)
            => new Picture(bmp);

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
    }
}