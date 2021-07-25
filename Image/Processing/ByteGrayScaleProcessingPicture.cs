using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Sharp.Image.Processing
{
    public unsafe class ByteGrayScaleProcessingPicture : IDisposable
    {
        internal ByteGrayScaleProcessingPicture() { }
        internal Bitmap bmp = null;
        internal byte* p = null;
        internal int width = -1;
        internal int height = -1;
        internal int stride = -1;
        internal BitmapData bmpdata = null;
        internal Picture picture = null;
        internal float[] data;
        internal bool closed = false;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}