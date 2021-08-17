using System;

namespace Flow.Image.Processing
{
    public static class PictureFlowExtension
    {
        public static Flow<Picture, Picture, Flow<Picture, Picture, P>> Copy<P>(this Flow<Picture, Picture, P> flow)
            => Flow.WithParent(
                Flow.From(flow, p => p.Copy()), 
                flow);
        
        public static Flow<BitmapPicture, Picture> OpenPixels(this Flow<Picture> flow)
            => Flow.From(flow, p => p as BitmapPicture);
        
        public static Flow<BitmapPicture, Picture, P> OpenPixels<T, P>(this Flow<Picture, T, P> flow)
            => Flow.From(flow, p => p as BitmapPicture);

        public static Flow<ByteProcessingPicture, BitmapPicture> ForPixel(this Flow<BitmapPicture> flow, 
            Func<byte, byte, byte, (int, int, int)> op) => Flow.From(flow, p => p.ForPixel(op));
        
        public static Flow<ByteProcessingPicture, BitmapPicture, P> ForPixel<T, P>(this Flow<BitmapPicture, T, P> flow, 
            Func<byte, byte, byte, (int, int, int)> op) => Flow.From(flow, p => p.ForPixel(op));
    }
}