using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;

namespace Flow.Image.Processing
{
    public unsafe class ByteProcessingPicture : BasePicture, IDisposable
    {
        internal ByteProcessingPicture() { }
        internal Bitmap bmp = null;
        internal byte* p = null;
        internal int width = -1;
        internal int height = -1;
        internal int stride = -1;
        internal BitmapData bmpdata = null;
        internal Picture picture = null;
        internal bool closed = false;
        internal static ByteProcessingPicture FromPicture(Picture picture)
        {
            if (picture == null)
                return null;
            
            ByteProcessingPicture pp = new ByteProcessingPicture();

            pp.picture = picture;
            pp.bmp = picture.bmp;
            pp.width = picture.rect.Width;
            pp.height = picture.rect.Height;
            pp.bmpdata = pp.bmp.LockBits(picture.rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            pp.stride = pp.bmpdata.Stride;
            pp.p = (byte*)pp.bmpdata.Scan0.ToPointer();

            return pp;
        }
        public override Bitmap ToBitmap()
            => Close().ToBitmap();
        public Picture Close()
        {
            if (closed)
                return null;
            closed = true;
            var pic = this.picture;
            Dispose();

            return pic;
        }
        internal FloatProcessingPicture ToFloatProcessing()
        {
            this.closed = true;
            
            var fpp = new FloatProcessingPicture();
            fpp.bmp = this.bmp;
            fpp.bmpdata = this.bmpdata;
            fpp.picture = this.picture;
            fpp.stride = this.stride;
            fpp.width = this.width;
            fpp.height = this.height;
            fpp.p = this.p;

            fpp.data = ArrayPool<float>.Shared.Rent(3 * fpp.stride * fpp.height);

            return fpp;
        }

        public void Dispose()
        {
            this.bmp.UnlockBits(this.bmpdata);
            this.picture = null;
            this.bmp = null;
            this.width = -1;
            this.height = -1;
            this.bmpdata = null;
            this.stride = -1;
            this.p = null;
        }
    }
}