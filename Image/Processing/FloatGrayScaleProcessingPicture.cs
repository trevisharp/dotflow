using System;
using System.Buffers;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace Flow.Image.Processing
{
    public unsafe class FloatGrayScaleProcessingPicture : Picture, IDisposable
    {
        internal FloatGrayScaleProcessingPicture() { }
        internal Bitmap bmp = null;
        internal byte* p = null;
        internal int width = -1;
        internal int height = -1;
        internal int stride = -1;
        internal BitmapData bmpdata = null;
        internal BitmapPicture picture = null;
        internal float[] data;
        internal bool closed = false;

        public override Bitmap ToBitmap()
            => Close().ToBitmap();
        public BitmapPicture Close()
        {
            if (this.closed)
                return null;
            this.closed = true;
            var pic = this.picture;

            unsafe
            {
                Parallel.For(0, this.height, j =>
                {
                    int start = j * this.stride;
                    byte* pixel = this.p + start;
                    for (int i = start, end = start + this.width; i < end; i++, pixel += 3)
                        pixel[0] = pixel[1] = pixel[2] = (byte)(255 * this.data[i]);
                });
            }
            this.bmp.UnlockBits(this.bmpdata);
            Dispose();
            return pic;
        }

        public void Dispose()
        {
            ArrayPool<float>.Shared.Return(this.data);
            this.width = -1;
            this.height = -1;
            this.stride = -1;
            this.bmpdata = null;
            this.bmp = null;
            this.picture = null;
            this.p = null;
            this.data = null;
        }
        
        public static FloatGrayScaleProcessingPicture FromPicture(BitmapPicture picture)
        {
            if (picture == null)
                return null;
            
            FloatGrayScaleProcessingPicture pp = new FloatGrayScaleProcessingPicture();

            pp.picture = picture;
            pp.bmp = picture.bmp;
            pp.width = picture.rect.Width;
            pp.height = picture.rect.Height;
            pp.bmpdata = pp.bmp.LockBits(picture.rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            pp.stride = pp.bmpdata.Stride;
            pp.p = (byte*)pp.bmpdata.Scan0.ToPointer();
            pp.data = ArrayPool<float>.Shared.Rent(pp.stride * pp.height);

            return pp;
        }
    }
}