using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Sharp.Image.Processing
{
    public unsafe class FloatProcessingPicture : IProcessingPicture, IDisposable
    {
        internal FloatProcessingPicture() { }
        internal Bitmap bmp = null;
        internal byte* p = null;
        internal int width = -1;
        internal int height = -1;
        internal int stride = -1;
        internal BitmapData bmpdata = null;
        internal Picture picture = null;
        internal float[] data;
        internal bool closed = false;
        
        internal static FloatProcessingPicture FromPicture(Picture picture)
        {
            if (picture == null)
                return null;
            
            FloatProcessingPicture pp = new FloatProcessingPicture();

            pp.picture = picture;
            pp.bmp = picture.bmp;
            pp.width = picture.rect.Width;
            pp.height = picture.rect.Height;
            pp.bmpdata = pp.bmp.LockBits(picture.rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            pp.stride = pp.bmpdata.Stride;
            pp.p = (byte*)pp.bmpdata.Scan0.ToPointer();
            pp.data = ArrayPool<float>.Shared.Rent(3 * pp.stride * pp.height);

            return pp;
        }
        
        public Picture Close()
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
                    int width = 3 * this.width;
                    for (int i = 0; i < width; i += 3, pixel += 3)
                    {
                        int index = j * this.stride + i;
                        pixel[2] = (byte)(255 * this.data[index]);
                        pixel[1] = (byte)(255 * this.data[index + 1]);
                        pixel[0] = (byte)(255 * this.data[index + 2]);
                    }
                });
            }
            Dispose();
            return pic;
        }
        internal ByteProcessingPicture ToByteProcessing()
        {
            this.closed = true;

            var bpp = new ByteProcessingPicture();
            bpp.bmp = this.bmp;
            bpp.bmpdata = this.bmpdata;
            bpp.picture = this.picture;
            bpp.stride = this.stride;
            bpp.width = this.width;
            bpp.height = this.height;
            bpp.p = this.p;

            return bpp;
        }

        public void Dispose()
        {
            ArrayPool<float>.Shared.Return(this.data);
            this.data = null;
            this.bmp.UnlockBits(this.bmpdata);
            this.picture = null;
            this.bmp = null;
            this.width = -1;
            this.height = -1;
            this.bmpdata = null;
            this.stride = -1;
            this.p = null;
        }
    
        public IntegralImage BuildIntegralImage()
        {
            throw new NotImplementedException();
        }
    }
}