using System.Drawing;
using System.Drawing.Imaging;

namespace Sharp.Image.Processing
{
    public unsafe class ByteProcessingPicture
    {
        private ByteProcessingPicture() { }
        internal Bitmap bmp = null;
        internal byte* p = null;
        internal int width = -1;
        internal int height = -1;
        internal int stride = -1;
        internal BitmapData bmpdata = null;
        internal Picture picture = null;
        internal bool closed = false;

        public static ByteProcessingPicture FromPicture(Picture picture)
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
        
        public Picture Close()
        {
            if (closed)
                return null;
            closed = true;
            var pic = this.picture;
            this.bmp.UnlockBits(this.bmpdata);

            this.picture = null;
            this.bmp = null;
            this.width = -1;
            this.height = -1;
            this.bmpdata = null;
            this.stride = -1;
            this.p = null;

            return pic;
        }
    }
}