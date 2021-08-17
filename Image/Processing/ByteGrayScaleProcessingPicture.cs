using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Flow.Image.Processing
{
    public unsafe class ByteGrayscaleProcessingPicture : Picture, IDisposable
    {
        internal ByteGrayscaleProcessingPicture() { }
        internal Bitmap bmp = null;
        internal byte* p = null;
        internal int width = -1;
        internal int height = -1;
        internal int stride = -1;
        internal BitmapData bmpdata = null;
        internal BitmapPicture picture = null;
        internal byte[] data;
        internal bool closed = false;
        internal static ByteGrayscaleProcessingPicture FromPicture(BitmapPicture picture)
        {
            if (picture == null)
                return null;
            
            ByteGrayscaleProcessingPicture pp = new ByteGrayscaleProcessingPicture();

            pp.picture = picture;
            pp.bmp = picture.bmp;
            pp.width = picture.rect.Width;
            pp.height = picture.rect.Height;
            pp.bmpdata = pp.bmp.LockBits(picture.rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            pp.stride = pp.bmpdata.Stride;
            pp.p = (byte*)pp.bmpdata.Scan0.ToPointer();
            pp.data = ArrayPool<byte>.Shared.Rent(pp.stride * pp.height);

            return pp;
        }
        public override Bitmap ToBitmap()
            => Close().ToBitmap();
        public BitmapPicture Close()
        {
            if (closed)
                return null;
            closed = true;
            var pic = this.picture;
            unsafe
            {
                Parallel.For(0, this.height, j =>
                {
                    byte* pixel = this.p + j * this.stride;
                    int end = j * this.stride + this.width;
                    for (int i = j * this.stride; i < end; i++, pixel += 3)
                        pixel[2] = pixel[1] = pixel[0] = this.data[i];
                });
            }
            this.bmp.UnlockBits(this.bmpdata);
            Dispose();
            return pic;
        }

        public void Dispose()
        {
            ArrayPool<byte>.Shared.Return(data);
            this.data = null;
            this.bmp = null;
            this.p = null;
            this.width = -1;
            this.height = -1;
            this.stride = -1;
            this.bmpdata = null;
            this.picture = null;
        }
    }
}